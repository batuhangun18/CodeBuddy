using Codebuddy.Application.DTOs.Submissions;
using Codebuddy.Application.Interfaces;
using Codebuddy.Domain.Entities;
using Codebuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Services;

public class SubmissionService : ISubmissionService
{
    private readonly CodebuddyDbContext _context;

    public SubmissionService(CodebuddyDbContext context)
    {
        _context = context;
    }

    public async Task<SubmissionDto> CreateAsync(Guid userId, CreateSubmissionRequest request)
    {
        var challenge = await _context.Challenges.FindAsync(request.ChallengeId);
        if (challenge is null)
        {
            throw new InvalidOperationException("Challenge not found.");
        }

        // Dummy evaluation for now.
        var passedCount = 1;
        var totalCount = 1;

        var submission = new Submission
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ChallengeId = request.ChallengeId,
            Code = request.Code,
            Language = request.Language,
            CreatedAt = DateTime.UtcNow,
            IsPassed = true,
            PassedTestCount = passedCount,
            TotalTestCount = totalCount
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();

        return new SubmissionDto
        {
            Id = submission.Id,
            ChallengeId = submission.ChallengeId,
            ChallengeTitle = challenge.Title,
            Language = submission.Language,
            Code = submission.Code,
            CreatedAt = submission.CreatedAt,
            IsPassed = submission.IsPassed,
            PassedTestCount = submission.PassedTestCount,
            TotalTestCount = submission.TotalTestCount
        };
    }

    public async Task<IEnumerable<SubmissionDto>> GetByUserAsync(Guid userId)
    {
        return await _context.Submissions
            .Include(s => s.Challenge)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => new SubmissionDto
            {
                Id = s.Id,
                ChallengeId = s.ChallengeId,
                ChallengeTitle = s.Challenge != null ? s.Challenge.Title : string.Empty,
                Language = s.Language,
                Code = s.Code,
                CreatedAt = s.CreatedAt,
                IsPassed = s.IsPassed,
                PassedTestCount = s.PassedTestCount,
                TotalTestCount = s.TotalTestCount
            })
            .ToListAsync();
    }

    public async Task<SubmissionDto?> GetByIdAsync(Guid id)
    {
        var submission = await _context.Submissions
            .Include(s => s.Challenge)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (submission is null)
        {
            return null;
        }

        return new SubmissionDto
        {
            Id = submission.Id,
            ChallengeId = submission.ChallengeId,
            ChallengeTitle = submission.Challenge != null ? submission.Challenge.Title : string.Empty,
            Language = submission.Language,
            Code = submission.Code,
            CreatedAt = submission.CreatedAt,
            IsPassed = submission.IsPassed,
            PassedTestCount = submission.PassedTestCount,
            TotalTestCount = submission.TotalTestCount
        };
    }
}
