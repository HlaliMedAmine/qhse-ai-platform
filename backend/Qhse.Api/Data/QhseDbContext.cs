using Microsoft.EntityFrameworkCore;
using Qhse.Api.Data.Seed;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Data;

public sealed class QhseDbContext(DbContextOptions<QhseDbContext> options) : DbContext(options)
{
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Audit> Audits => Set<Audit>();
    public DbSet<NonConformity> NonConformities => Set<NonConformity>();
    public DbSet<Risk> Risks => Set<Risk>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<AiAnalysisLog> AiAnalysisLogs => Set<AiAnalysisLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QhseDbContext).Assembly);
        QhseSeedData.Seed(modelBuilder);
    }
}
