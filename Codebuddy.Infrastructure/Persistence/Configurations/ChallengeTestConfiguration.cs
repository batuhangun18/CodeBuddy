using Codebuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebuddy.Infrastructure.Persistence.Configurations;

public class ChallengeTestConfiguration : IEntityTypeConfiguration<ChallengeTest>
{
    public void Configure(EntityTypeBuilder<ChallengeTest> builder)
    {
        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.TestCode)
            .IsRequired();

        builder.HasOne(ct => ct.Challenge)
            .WithMany(c => c.Tests)
            .HasForeignKey(ct => ct.ChallengeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
