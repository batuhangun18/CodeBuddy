using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Challenges;

public class ChallengeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChallengeDifficulty Difficulty { get; set; }
    public string Language { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}
