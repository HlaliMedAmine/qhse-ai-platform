using Microsoft.EntityFrameworkCore;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Data.Seed;

public static class QhseSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var createdAt = DateTimeOffset.Parse("2025-05-16T00:00:00+00:00");
        var incidentId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var auditId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        modelBuilder.Entity<Incident>().HasData(
            new Incident
            {
                Id = incidentId,
                Reference = "INC-2041",
                Title = "Fuite chimique zone B",
                Site = "Usine Lyon",
                Severity = Severity.Critical,
                Status = RecordStatus.InProgress,
                ReportedBy = "M. Dupont",
                IncidentDate = new DateOnly(2025, 5, 14),
                Description = "Fuite detectee dans la zone de stockage chimique.",
                CreatedAtUtc = createdAt
            },
            new Incident
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                Reference = "INC-2040",
                Title = "Chute de plain-pied entrepot",
                Site = "Entrepot Lille",
                Severity = Severity.Medium,
                Status = RecordStatus.Resolved,
                ReportedBy = "S. Martin",
                IncidentDate = new DateOnly(2025, 5, 12),
                CreatedAtUtc = createdAt
            });

        modelBuilder.Entity<Audit>().HasData(
            new Audit
            {
                Id = auditId,
                Reference = "ISO 14001 interne",
                Scope = "Environnement - Marseille",
                Auditor = "E. Lambert",
                AuditDate = new DateOnly(2025, 5, 15),
                Score = 92,
                Status = AuditStatus.Completed,
                CreatedAtUtc = createdAt
            },
            new Audit
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                Reference = "ISO 45001 - T2",
                Scope = "Securite - Usine Lyon",
                Auditor = "Cabinet Veritas",
                AuditDate = new DateOnly(2025, 6, 2),
                Score = 87,
                Status = AuditStatus.Planned,
                CreatedAtUtc = createdAt
            });

        modelBuilder.Entity<NonConformity>().HasData(
            new NonConformity
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Reference = "NC-511",
                Title = "Etiquetage produit chimique manquant",
                SourceReference = "INC-2041",
                Severity = Severity.Critical,
                Owner = "Resp. HSE",
                DueDate = new DateOnly(2025, 5, 30),
                Status = RecordStatus.InProgress,
                IncidentId = incidentId,
                CreatedAtUtc = createdAt
            },
            new NonConformity
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                Reference = "NC-510",
                Title = "Registre des dechets incomplet",
                SourceReference = "ISO 14001 interne",
                Severity = Severity.Medium,
                Owner = "Resp. Environnement",
                DueDate = new DateOnly(2025, 6, 15),
                Status = RecordStatus.Open,
                AuditId = auditId,
                CreatedAtUtc = createdAt
            });

        modelBuilder.Entity<Risk>().HasData(
            new Risk
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444441"),
                Reference = "R-001",
                Hazard = "Exposition produits chimiques",
                Category = RiskCategory.Safety,
                Likelihood = 4,
                Impact = 5,
                Mitigation = "EPI, ventilation et formation trimestrielle",
                CreatedAtUtc = createdAt
            },
            new Risk
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444442"),
                Reference = "R-003",
                Hazard = "Rejet aqueux hors normes",
                Category = RiskCategory.Environment,
                Likelihood = 2,
                Impact = 5,
                Mitigation = "Controle continu et station d'epuration",
                CreatedAtUtc = createdAt
            });

        modelBuilder.Entity<Report>().HasData(
            new Report
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555551"),
                Reference = "RPT-088",
                Name = "Rapport mensuel HSE - Avril 2025",
                Type = "PDF",
                UploadedBy = "E. Lambert",
                UploadedAt = new DateOnly(2025, 5, 2),
                SizeKb = 842,
                CreatedAtUtc = createdAt
            },
            new Report
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555552"),
                Reference = "RPT-086",
                Name = "Analyse incidents Q1",
                Type = "DOCX",
                UploadedBy = "S. Martin",
                UploadedAt = new DateOnly(2025, 4, 12),
                SizeKb = 332,
                CreatedAtUtc = createdAt
            });
    }
}
