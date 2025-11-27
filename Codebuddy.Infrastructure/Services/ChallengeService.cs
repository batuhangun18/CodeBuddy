using Codebuddy.Application.Common;
using Codebuddy.Application.DTOs.Challenges;
using Codebuddy.Application.Interfaces;
using Codebuddy.Domain.Entities;
using Codebuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Services;

public class ChallengeService : IChallengeService
{
    private readonly CodebuddyDbContext _context;

    public ChallengeService(CodebuddyDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ChallengeDto>> GetChallengesAsync(ChallengeFilterRequest filter)
    {
        var query = _context.Challenges
            .Include(c => c.CreatedByUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Language))
        {
            query = query.Where(c => c.Language == filter.Language);
        }

        if (filter.Difficulty.HasValue)
        {
            query = query.Where(c => c.Difficulty == filter.Difficulty.Value);
        }

        var page = filter.Page < 1 ? 1 : filter.Page;
        var pageSize = filter.PageSize is < 1 or > 100 ? 10 : filter.PageSize;

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ChallengeDto
            {
                Id = c.Id,
                Title = c.Title,
                Slug = c.Slug,
                Description = c.Description,
                Difficulty = c.Difficulty,
                Language = c.Language,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedByUser != null ? c.CreatedByUser.UserName : string.Empty
            })
            .ToListAsync();

        return new PagedResult<ChallengeDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = total
        };
    }

    public async Task<ChallengeDto?> GetBySlugAsync(string slug)
    {
        var challenge = await _context.Challenges
            .Include(c => c.CreatedByUser)
            .FirstOrDefaultAsync(c => c.Slug == slug);

        return challenge is null
            ? null
            : new ChallengeDto
            {
                Id = challenge.Id,
                Title = challenge.Title,
                Slug = challenge.Slug,
                Description = challenge.Description,
                Difficulty = challenge.Difficulty,
                Language = challenge.Language,
                CreatedAt = challenge.CreatedAt,
                CreatedBy = challenge.CreatedByUser?.UserName
            };
    }

    public async Task<IEnumerable<ChallengeTestDto>> GetTestsAsync(Guid challengeId, bool publicOnly = true)
    {
        var query = _context.ChallengeTests.Where(t => t.ChallengeId == challengeId);
        if (publicOnly)
        {
            query = query.Where(t => t.IsPublic);
        }

        return await query.Select(t => new ChallengeTestDto
        {
            Id = t.Id,
            TestCode = t.TestCode,
            IsPublic = t.IsPublic
        }).ToListAsync();
    }

    public async Task<ChallengeDto> CreateAsync(CreateChallengeRequest request, Guid userId)
    {
        var exists = await _context.Challenges.AnyAsync(c => c.Slug == request.Slug);
        if (exists)
        {
            throw new InvalidOperationException("Slug already exists.");
        }

        var challenge = new Challenge
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Slug = request.Slug,
            Description = request.Description,
            Difficulty = request.Difficulty,
            Language = request.Language,
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Challenges.Add(challenge);
        await _context.SaveChangesAsync();

        return new ChallengeDto
        {
            Id = challenge.Id,
            Title = challenge.Title,
            Slug = challenge.Slug,
            Description = challenge.Description,
            Difficulty = challenge.Difficulty,
            Language = challenge.Language,
            CreatedAt = challenge.CreatedAt,
            CreatedBy = null
        };
    }
}
