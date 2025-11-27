namespace Codebuddy.Application.DTOs.Discussions;

public class DiscussionThreadDto
{
    public Guid Id { get; set; }
    public Guid ChallengeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
