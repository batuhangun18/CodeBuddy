namespace Codebuddy.Domain.Entities;

public class Submission
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ChallengeId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsPassed { get; set; }
    public int PassedTestCount { get; set; }
    public int TotalTestCount { get; set; }

    public User? User { get; set; }
    public Challenge? Challenge { get; set; }
}
