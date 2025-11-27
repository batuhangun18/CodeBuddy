using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Buddies;

public class BuddySessionDto
{
    public Guid Id { get; set; }
    public Guid ChallengeId { get; set; }
    public string ChallengeTitle { get; set; } = string.Empty;
    public Guid User1Id { get; set; }
    public Guid? User2Id { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public BuddySessionStatus Status { get; set; }
}
