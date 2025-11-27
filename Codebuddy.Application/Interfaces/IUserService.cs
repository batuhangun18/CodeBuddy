using Codebuddy.Application.DTOs.Users;

namespace Codebuddy.Application.Interfaces;

public interface IUserService
{
    Task<UserProfileDto?> GetCurrentUserAsync(Guid userId);
    Task<UserProfileDto?> UpdateProfileAsync(Guid userId, UpdateProfileRequest request);
}
