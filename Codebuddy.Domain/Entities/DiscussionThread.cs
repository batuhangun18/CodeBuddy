namespace Codebuddy.Domain.Entities;

public class DiscussionThread
{
    public Guid Id { get; set; }
    public Guid ChallengeId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Challenge? Challenge { get; set; }
    public User? CreatedByUser { get; set; }
    public ICollection<DiscussionMessage> Messages { get; set; } = new List<DiscussionMessage>();
}
