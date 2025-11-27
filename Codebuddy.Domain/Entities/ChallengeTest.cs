namespace Codebuddy.Domain.Entities;

public class ChallengeTest
{
    public Guid Id { get; set; }
    public Guid ChallengeId { get; set; }
    public string TestCode { get; set; } = string.Empty;
    public bool IsPublic { get; set; }

    public Challenge? Challenge { get; set; }
}
