using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data.Configurations;

public sealed class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.ToTable("Incidents");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Reference).IsUnique();

        builder.Property(x => x.Reference).HasMaxLength(40).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(180).IsRequired();
        builder.Property(x => x.Site).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Severity).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(x => x.ReportedBy).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000);
    }
}
