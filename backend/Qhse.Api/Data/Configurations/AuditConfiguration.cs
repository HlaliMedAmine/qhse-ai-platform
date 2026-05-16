using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data.Configurations;

public sealed class AuditConfiguration : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("Audits");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Reference).IsUnique();

        builder.Property(x => x.Reference).HasMaxLength(160).IsRequired();
        builder.Property(x => x.Scope).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Auditor).HasMaxLength(120).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
    }
}
