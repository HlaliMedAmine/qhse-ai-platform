using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data.Configurations;

public sealed class AiAnalysisLogConfiguration : IEntityTypeConfiguration<AiAnalysisLog>
{
    public void Configure(EntityTypeBuilder<AiAnalysisLog> builder)
    {
        builder.ToTable("AiAnalysisLogs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.InputText).HasMaxLength(8000).IsRequired();
        builder.Property(x => x.Summary).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.RiskLevel).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(x => x.CorrectiveActionsJson).IsRequired();
        builder.Property(x => x.RecommendationsJson).IsRequired();
        builder.Property(x => x.Provider).HasMaxLength(80).IsRequired();
    }
}
