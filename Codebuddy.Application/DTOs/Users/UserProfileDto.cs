using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Users;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SubscriptionType SubscriptionType { get; set; }
    public string? AvatarUrl { get; set; }
    public string? DisplayColor { get; set; }
    public DateTime CreatedAt { get; set; }
}
