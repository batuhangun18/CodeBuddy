using Codebuddy.Domain.Enums;

namespace Codebuddy.Application.DTOs.Auth;

public class AuthResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SubscriptionType SubscriptionType { get; set; }
    public string Token { get; set; } = string.Empty;
}
