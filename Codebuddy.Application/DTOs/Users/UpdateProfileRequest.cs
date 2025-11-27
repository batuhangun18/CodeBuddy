using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Users;

public class UpdateProfileRequest
{
    public string? UserName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? DisplayColor { get; set; }
    public SubscriptionType? SubscriptionType { get; set; }
}
