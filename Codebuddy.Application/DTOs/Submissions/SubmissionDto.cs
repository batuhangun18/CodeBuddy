namespace Codebuddy.Application.DTOs.Submissions;

public class SubmissionDto
{
    public Guid Id { get; set; }
    public Guid ChallengeId { get; set; }
    public string ChallengeTitle { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsPassed { get; set; }
    public int PassedTestCount { get; set; }
    public int TotalTestCount { get; set; }
}
