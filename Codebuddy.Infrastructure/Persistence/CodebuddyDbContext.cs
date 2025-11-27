using Codebuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Persistence;

public class CodebuddyDbContext : DbContext
{
    public CodebuddyDbContext(DbContextOptions<CodebuddyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<ChallengeTest> ChallengeTests => Set<ChallengeTest>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<BuddySession> BuddySessions => Set<BuddySession>();
    public DbSet<DiscussionThread> DiscussionThreads => Set<DiscussionThread>();
    public DbSet<DiscussionMessage> DiscussionMessages => Set<DiscussionMessage>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<UserBadge> UserBadges => Set<UserBadge>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodebuddyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

// Migration helpers:
// dotnet ef migrations add InitialCreate --project Codebuddy.Infrastructure --startup-project Codebuddy.Api
// dotnet ef database update --project Codebuddy.Infrastructure --startup-project Codebuddy.Api
