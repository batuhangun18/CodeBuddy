using Codebuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebuddy.Infrastructure.Persistence.Configurations;

public class BuddySessionConfiguration : IEntityTypeConfiguration<BuddySession>
{
    public void Configure(EntityTypeBuilder<BuddySession> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.Challenge)
            .WithMany(c => c.BuddySessions)
            .HasForeignKey(b => b.ChallengeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.User1)
            .WithMany(u => u.BuddySessionsAsUser1)
            .HasForeignKey(b => b.User1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.User2)
            .WithMany(u => u.BuddySessionsAsUser2)
            .HasForeignKey(b => b.User2Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
