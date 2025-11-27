namespace Codebuddy.Application.DTOs.Buddies;

public class CreateBuddySessionRequest
{
    public Guid ChallengeId { get; set; }
    public Guid? BuddyUserId { get; set; }
}
