using Codebuddy.Application.DTOs.Buddies;
using Codebuddy.Application.Interfaces;
using Codebuddy.Domain.Entities;
using Codebuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Services;

public class BuddyService : IBuddyService
{
    private readonly CodebuddyDbContext _context;

    public BuddyService(CodebuddyDbContext context)
    {
        _context = context;
    }

    public async Task<BuddySessionDto> CreateSessionAsync(Guid userId, CreateBuddySessionRequest request)
    {
        var challenge = await _context.Challenges.FindAsync(request.ChallengeId);
        if (challenge is null)
        {
            throw new InvalidOperationException("Challenge not found.");
        }

        var session = new BuddySession
        {
            Id = Guid.NewGuid(),
            ChallengeId = request.ChallengeId,
            User1Id = userId,
            User2Id = request.BuddyUserId,
            StartedAt = DateTime.UtcNow
        };

        _context.BuddySessions.Add(session);
        await _context.SaveChangesAsync();

        return new BuddySessionDto
        {
            Id = session.Id,
            ChallengeId = session.ChallengeId,
            ChallengeTitle = challenge.Title,
            User1Id = session.User1Id,
            User2Id = session.User2Id,
            StartedAt = session.StartedAt,
            FinishedAt = session.FinishedAt,
            Status = session.Status
        };
    }

    public async Task<IEnumerable<BuddySessionDto>> GetUserSessionsAsync(Guid userId)
    {
        return await _context.BuddySessions
            .Include(b => b.Challenge)
            .Where(b => b.User1Id == userId || b.User2Id == userId)
            .OrderByDescending(b => b.StartedAt)
            .Select(b => new BuddySessionDto
            {
                Id = b.Id,
                ChallengeId = b.ChallengeId,
                ChallengeTitle = b.Challenge != null ? b.Challenge.Title : string.Empty,
                User1Id = b.User1Id,
                User2Id = b.User2Id,
                StartedAt = b.StartedAt,
                FinishedAt = b.FinishedAt,
                Status = b.Status
            })
            .ToListAsync();
    }
}
