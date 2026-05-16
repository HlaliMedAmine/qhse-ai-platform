using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data.Configurations;

public sealed class NonConformityConfiguration : IEntityTypeConfiguration<NonConformity>
{
    public void Configure(EntityTypeBuilder<NonConformity> builder)
    {
        builder.ToTable("NonConformities");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Reference).IsUnique();

        builder.Property(x => x.Reference).HasMaxLength(40).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(180).IsRequired();
        builder.Property(x => x.SourceReference).HasMaxLength(60).IsRequired();
        builder.Property(x => x.Severity).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.Owner).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();

        builder
            .HasOne(x => x.Incident)
            .WithMany(x => x.NonConformities)
            .HasForeignKey(x => x.IncidentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Audit)
            .WithMany(x => x.NonConformities)
            .HasForeignKey(x => x.AuditId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
