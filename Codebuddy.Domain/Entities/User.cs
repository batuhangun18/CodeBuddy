using Codebuddy.Domain.Enums;

namespace Codebuddy.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Free;
    public string? AvatarUrl { get; set; }
    public string? DisplayColor { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Challenge> ChallengesCreated { get; set; } = new List<Challenge>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    public ICollection<BuddySession> BuddySessionsAsUser1 { get; set; } = new List<BuddySession>();
    public ICollection<BuddySession> BuddySessionsAsUser2 { get; set; } = new List<BuddySession>();
    public ICollection<DiscussionThread> DiscussionThreads { get; set; } = new List<DiscussionThread>();
    public ICollection<DiscussionMessage> DiscussionMessages { get; set; } = new List<DiscussionMessage>();
    public ICollection<UserBadge> Badges { get; set; } = new List<UserBadge>();
}
