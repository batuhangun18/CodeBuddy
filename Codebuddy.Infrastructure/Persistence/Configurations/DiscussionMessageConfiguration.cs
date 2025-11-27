using Codebuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebuddy.Infrastructure.Persistence.Configurations;

public class DiscussionMessageConfiguration : IEntityTypeConfiguration<DiscussionMessage>
{
    public void Configure(EntityTypeBuilder<DiscussionMessage> builder)
    {
        builder.HasKey(dm => dm.Id);

        builder.Property(dm => dm.Message)
            .IsRequired();

        builder.HasOne(dm => dm.Thread)
            .WithMany(t => t.Messages)
            .HasForeignKey(dm => dm.ThreadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dm => dm.User)
            .WithMany(u => u.DiscussionMessages)
            .HasForeignKey(dm => dm.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
