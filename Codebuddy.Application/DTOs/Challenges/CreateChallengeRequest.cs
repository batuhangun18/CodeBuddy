using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Challenges;

public class CreateChallengeRequest
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChallengeDifficulty Difficulty { get; set; }
    public string Language { get; set; } = string.Empty;
}
