using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data.Configurations;

public sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Reference).IsUnique();

        builder.Property(x => x.Reference).HasMaxLength(40).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(220).IsRequired();
        builder.Property(x => x.Type).HasMaxLength(20).IsRequired();
        builder.Property(x => x.UploadedBy).HasMaxLength(120).IsRequired();
        builder.Property(x => x.BlobUri).HasMaxLength(1000);
    }
}
