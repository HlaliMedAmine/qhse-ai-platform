using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data.Configurations;

public sealed class RiskConfiguration : IEntityTypeConfiguration<Risk>
{
    public void Configure(EntityTypeBuilder<Risk> builder)
    {
        builder.ToTable("Risks");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Reference).IsUnique();

        builder.Property(x => x.Reference).HasMaxLength(40).IsRequired();
        builder.Property(x => x.Hazard).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Category).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(x => x.Mitigation).HasMaxLength(500).IsRequired();
        builder.Ignore(x => x.Score);
    }
}
