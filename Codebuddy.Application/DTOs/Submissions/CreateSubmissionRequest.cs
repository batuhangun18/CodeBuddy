namespace Codebuddy.Application.DTOs.Submissions;

public class CreateSubmissionRequest
{
    public Guid ChallengeId { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
