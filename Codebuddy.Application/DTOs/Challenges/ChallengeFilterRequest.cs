using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Challenges;

public class ChallengeFilterRequest
{
    public string? Language { get; set; }
    public ChallengeDifficulty? Difficulty { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
