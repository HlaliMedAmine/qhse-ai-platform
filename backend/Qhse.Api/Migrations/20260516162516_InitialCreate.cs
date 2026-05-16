using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qhse.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiAnalysisLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputText = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    RiskLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CorrectiveActionsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecommendationsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiAnalysisLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Auditor = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    AuditDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    Site = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReportedBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IncidentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    UploadedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    SizeKb = table.Column<int>(type: "int", nullable: false),
                    BlobUri = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Hazard = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Likelihood = table.Column<int>(type: "int", nullable: false),
                    Impact = table.Column<int>(type: "int", nullable: false),
                    Mitigation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonConformities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    SourceReference = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuditId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonConformities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonConformities_Audits_AuditId",
                        column: x => x.AuditId,
                        principalTable: "Audits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NonConformities_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AuditDate", "Auditor", "CreatedAtUtc", "Reference", "Scope", "Score", "Status", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateOnly(2025, 5, 15), "E. Lambert", new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ISO 14001 interne", "Environnement - Marseille", 92, "Completed", null },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new DateOnly(2025, 6, 2), "Cabinet Veritas", new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ISO 45001 - T2", "Securite - Usine Lyon", 87, "Planned", null }
                });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "Id", "CreatedAtUtc", "Description", "IncidentDate", "Reference", "ReportedBy", "Severity", "Site", "Status", "Title", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Fuite detectee dans la zone de stockage chimique.", new DateOnly(2025, 5, 14), "INC-2041", "M. Dupont", "Critical", "Usine Lyon", "InProgress", "Fuite chimique zone B", null },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateOnly(2025, 5, 12), "INC-2040", "S. Martin", "Medium", "Entrepot Lille", "Resolved", "Chute de plain-pied entrepot", null }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "BlobUri", "CreatedAtUtc", "Name", "Reference", "SizeKb", "Type", "UpdatedAtUtc", "UploadedAt", "UploadedBy" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555551"), null, new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Rapport mensuel HSE - Avril 2025", "RPT-088", 842, "PDF", null, new DateOnly(2025, 5, 2), "E. Lambert" },
                    { new Guid("55555555-5555-5555-5555-555555555552"), null, new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Analyse incidents Q1", "RPT-086", 332, "DOCX", null, new DateOnly(2025, 4, 12), "S. Martin" }
                });

            migrationBuilder.InsertData(
                table: "Risks",
                columns: new[] { "Id", "Category", "CreatedAtUtc", "Hazard", "Impact", "Likelihood", "Mitigation", "Reference", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444441"), "Safety", new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Exposition produits chimiques", 5, 4, "EPI, ventilation et formation trimestrielle", "R-001", null },
                    { new Guid("44444444-4444-4444-4444-444444444442"), "Environment", new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Rejet aqueux hors normes", 5, 2, "Controle continu et station d'epuration", "R-003", null }
                });

            migrationBuilder.InsertData(
                table: "NonConformities",
                columns: new[] { "Id", "AuditId", "CreatedAtUtc", "DueDate", "IncidentId", "Owner", "Reference", "Severity", "SourceReference", "Status", "Title", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(2025, 5, 30), new Guid("11111111-1111-1111-1111-111111111111"), "Resp. HSE", "NC-511", "Critical", "INC-2041", "InProgress", "Etiquetage produit chimique manquant", null },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(2025, 6, 15), null, "Resp. Environnement", "NC-510", "Medium", "ISO 14001 interne", "Open", "Registre des dechets incomplet", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audits_Reference",
                table: "Audits",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_Reference",
                table: "Incidents",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonConformities_AuditId",
                table: "NonConformities",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_NonConformities_IncidentId",
                table: "NonConformities",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_NonConformities_Reference",
                table: "NonConformities",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Reference",
                table: "Reports",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Risks_Reference",
                table: "Risks",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiAnalysisLogs");

            migrationBuilder.DropTable(
                name: "NonConformities");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "Incidents");
        }
    }
}
