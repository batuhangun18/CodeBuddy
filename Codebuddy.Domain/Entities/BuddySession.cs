using Codebuddy.Domain.Enums;

namespace Codebuddy.Domain.Entities;

public class BuddySession
{
    public Guid Id { get; set; }
    public Guid ChallengeId { get; set; }
    public Guid User1Id { get; set; }
    public Guid? User2Id { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }
    public BuddySessionStatus Status { get; set; } = BuddySessionStatus.Pending;

    public Challenge? Challenge { get; set; }
    public User? User1 { get; set; }
    public User? User2 { get; set; }
}
