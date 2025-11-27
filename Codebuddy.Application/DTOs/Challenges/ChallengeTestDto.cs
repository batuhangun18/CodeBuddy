namespace Codebuddy.Application.DTOs.Challenges;

public class ChallengeTestDto
{
    public Guid Id { get; set; }
    public string TestCode { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
}
