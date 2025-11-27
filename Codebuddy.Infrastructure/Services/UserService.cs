using Codebuddy.Application.DTOs.Users;
using Codebuddy.Application.Interfaces;
using Codebuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly CodebuddyDbContext _context;

    public UserService(CodebuddyDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDto?> GetCurrentUserAsync(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user is null
            ? null
            : new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                SubscriptionType = user.SubscriptionType,
                AvatarUrl = user.AvatarUrl,
                DisplayColor = user.DisplayColor,
                CreatedAt = user.CreatedAt
            };
    }

    public async Task<UserProfileDto?> UpdateProfileAsync(Guid userId, UpdateProfileRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            user.UserName = request.UserName;
        }

        if (request.SubscriptionType.HasValue)
        {
            user.SubscriptionType = request.SubscriptionType.Value;
        }

        user.AvatarUrl = request.AvatarUrl ?? user.AvatarUrl;
        user.DisplayColor = request.DisplayColor ?? user.DisplayColor;

        await _context.SaveChangesAsync();

        return new UserProfileDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            SubscriptionType = user.SubscriptionType,
            AvatarUrl = user.AvatarUrl,
            DisplayColor = user.DisplayColor,
            CreatedAt = user.CreatedAt
        };
    }
}
