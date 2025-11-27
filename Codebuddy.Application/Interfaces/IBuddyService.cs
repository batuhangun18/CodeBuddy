using Codebuddy.Application.DTOs.Buddies;

namespace Codebuddy.Application.Interfaces;

public interface IBuddyService
{
    Task<BuddySessionDto> CreateSessionAsync(Guid userId, CreateBuddySessionRequest request);
    Task<IEnumerable<BuddySessionDto>> GetUserSessionsAsync(Guid userId);
}
