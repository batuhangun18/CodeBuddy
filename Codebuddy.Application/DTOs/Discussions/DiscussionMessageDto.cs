namespace Codebuddy.Application.DTOs.Discussions;

public class DiscussionMessageDto
{
    public Guid Id { get; set; }
    public Guid ThreadId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
