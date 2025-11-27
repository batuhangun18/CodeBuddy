using Codebuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebuddy.Infrastructure.Persistence.Configurations;

public class DiscussionThreadConfiguration : IEntityTypeConfiguration<DiscussionThread>
{
    public void Configure(EntityTypeBuilder<DiscussionThread> builder)
    {
        builder.HasKey(dt => dt.Id);

        builder.Property(dt => dt.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(dt => dt.Challenge)
            .WithMany(c => c.DiscussionThreads)
            .HasForeignKey(dt => dt.ChallengeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dt => dt.CreatedByUser)
            .WithMany(u => u.DiscussionThreads)
            .HasForeignKey(dt => dt.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
