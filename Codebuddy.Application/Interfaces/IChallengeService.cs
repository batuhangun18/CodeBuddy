using Codebuddy.Application.Common;
using Codebuddy.Application.DTOs.Challenges;

namespace Codebuddy.Application.Interfaces;

public interface IChallengeService
{
    Task<PagedResult<ChallengeDto>> GetChallengesAsync(ChallengeFilterRequest filter);
    Task<ChallengeDto?> GetBySlugAsync(string slug);
    Task<IEnumerable<ChallengeTestDto>> GetTestsAsync(Guid challengeId, bool publicOnly = true);
    Task<ChallengeDto> CreateAsync(CreateChallengeRequest request, Guid userId);
}
