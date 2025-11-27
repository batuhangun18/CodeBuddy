namespace Codebuddy.Domain.Entities;

public class DiscussionMessage
{
    public Guid Id { get; set; }
    public Guid ThreadId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DiscussionThread? Thread { get; set; }
    public User? User { get; set; }
}
