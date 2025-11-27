using Codebuddy.Domain.Enums;

namespace Codebuddy.Domain.Entities;

public class Challenge
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ChallengeDifficulty Difficulty { get; set; } = ChallengeDifficulty.Level0;
    public string Language { get; set; } = string.Empty;
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? CreatedByUser { get; set; }
    public ICollection<ChallengeTest> Tests { get; set; } = new List<ChallengeTest>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    public ICollection<BuddySession> BuddySessions { get; set; } = new List<BuddySession>();
    public ICollection<DiscussionThread> DiscussionThreads { get; set; } = new List<DiscussionThread>();
}
