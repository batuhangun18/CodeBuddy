using Codebuddy.Application.DTOs.Discussions;
using Codebuddy.Application.Interfaces;
using Codebuddy.Domain.Entities;
using Codebuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Services;

public class DiscussionService : IDiscussionService
{
    private readonly CodebuddyDbContext _context;

    public DiscussionService(CodebuddyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DiscussionThreadDto>> GetThreadsAsync(Guid challengeId)
    {
        return await _context.DiscussionThreads
            .Include(t => t.CreatedByUser)
            .Where(t => t.ChallengeId == challengeId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new DiscussionThreadDto
            {
                Id = t.Id,
                ChallengeId = t.ChallengeId,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                CreatedBy = t.CreatedByUser != null ? t.CreatedByUser.UserName : string.Empty
            })
            .ToListAsync();
    }

    public async Task<DiscussionThreadDto> CreateThreadAsync(Guid challengeId, Guid userId, CreateThreadRequest request)
    {
        var challengeExists = await _context.Challenges.AnyAsync(c => c.Id == challengeId);
        if (!challengeExists)
        {
            throw new InvalidOperationException("Challenge not found.");
        }

        var thread = new DiscussionThread
        {
            Id = Guid.NewGuid(),
            ChallengeId = challengeId,
            CreatedByUserId = userId,
            Title = request.Title,
            CreatedAt = DateTime.UtcNow
        };

        _context.DiscussionThreads.Add(thread);

        if (!string.IsNullOrWhiteSpace(request.Message))
        {
            _context.DiscussionMessages.Add(new DiscussionMessage
            {
                Id = Guid.NewGuid(),
                ThreadId = thread.Id,
                UserId = userId,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();

        var user = await _context.Users.FindAsync(userId);

        return new DiscussionThreadDto
        {
            Id = thread.Id,
            ChallengeId = thread.ChallengeId,
            Title = thread.Title,
            CreatedAt = thread.CreatedAt,
            CreatedBy = user?.UserName ?? string.Empty
        };
    }

    public async Task<IEnumerable<DiscussionMessageDto>> GetMessagesAsync(Guid threadId)
    {
        return await _context.DiscussionMessages
            .Include(m => m.User)
            .Where(m => m.ThreadId == threadId)
            .OrderBy(m => m.CreatedAt)
            .Select(m => new DiscussionMessageDto
            {
                Id = m.Id,
                ThreadId = m.ThreadId,
                UserName = m.User != null ? m.User.UserName : string.Empty,
                Message = m.Message,
                CreatedAt = m.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<DiscussionMessageDto> CreateMessageAsync(Guid threadId, Guid userId, CreateMessageRequest request)
    {
        var threadExists = await _context.DiscussionThreads.AnyAsync(t => t.Id == threadId);
        if (!threadExists)
        {
            throw new InvalidOperationException("Thread not found.");
        }

        var message = new DiscussionMessage
        {
            Id = Guid.NewGuid(),
            ThreadId = threadId,
            UserId = userId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        _context.DiscussionMessages.Add(message);
        await _context.SaveChangesAsync();

        var user = await _context.Users.FindAsync(userId);

        return new DiscussionMessageDto
        {
            Id = message.Id,
            ThreadId = message.ThreadId,
            UserName = user?.UserName ?? string.Empty,
            Message = message.Message,
            CreatedAt = message.CreatedAt
        };
    }
}
