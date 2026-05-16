# QHSE AI Platform Backend

ASP.NET Core Web API backend for the existing React QHSE AI frontend MVP.

## Frontend Analysis

The React app currently uses TanStack routes and local mock data:

| Frontend route | Backend module | API |
| --- | --- | --- |
| `/` dashboard | aggregate data later | existing module APIs |
| `/incidents` | Incidents | `GET /api/incidents` |
| `/audits` | Audits | `GET /api/audits` |
| `/nonconformities` | Non-conformities | `GET /api/nonconformities` |
| `/risks` | Risks | `GET /api/risks` |
| `/reports` | Reports | `GET /api/reports` |
| `/ai-assistant` | AI Assistant | `POST /api/ai/analyze` |

The frontend data shape came from `src/lib/qhse/mock-data.ts`: incidents, audits, non-conformities, risks, reports, risk scoring, and AI analysis suggestions.

## Solution Structure

```text
backend/
  Qhse.sln
  Qhse.Api/
    Auth/                    Future JWT-ready current-user abstraction
    Contracts/               Request/response DTOs
    Controllers/             REST API controllers
    Data/                    EF Core DbContext, configurations, seed data
    Domain/                  Entity models and enums
    Infrastructure/          EF repository implementation
    Migrations/              Initial EF Core migration
    Options/                 Azure OpenAI configuration
    Services/                Business logic and AI analysis orchestration
```

## Stack

- .NET 8 ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server / LocalDB
- Swagger via Swashbuckle
- CORS for React dev origins
- Repository + service layering
- Mock Azure OpenAI service, ready for real provider integration

## Key Endpoints

```http
GET    /api/health
GET    /api/incidents
POST   /api/incidents
GET    /api/audits
POST   /api/audits
GET    /api/nonconformities
POST   /api/nonconformities
GET    /api/risks
POST   /api/risks
GET    /api/reports
POST   /api/reports
POST   /api/ai/analyze
```

## AI Assistant Endpoint

`POST /api/ai/analyze`

Request:

```json
{
  "text": "Fuite chimique detectee en zone B avec interruption de production.",
  "sourceType": "incident",
  "sourceReference": "INC-2041"
}
```

Response:

```json
{
  "summary": "Mock analysis: the submitted QHSE text indicates a Critical risk profile and requires documented follow-up.",
  "riskLevel": "Critical",
  "correctiveActions": [
    "Secure the affected area and confirm immediate controls are in place.",
    "Assign an accountable owner and target closure date.",
    "Record root-cause analysis and evidence of corrective action completion."
  ],
  "recommendations": [
    "Review the relevant procedure and training records.",
    "Trend similar events across sites for recurring causes.",
    "Escalate to QHSE leadership if residual risk remains High or Critical."
  ],
  "provider": "MockAzureOpenAI",
  "generatedAtUtc": "2026-05-16T16:30:00Z"
}
```

## Configuration

`Qhse.Api/appsettings.json` contains:

- `ConnectionStrings:QhseDatabase`
- `Cors:AllowedOrigins`
- `AzureOpenAI:Endpoint`
- `AzureOpenAI:DeploymentName`
- `AzureOpenAI:ApiKey`
- `AzureOpenAI:UseMock`

For Azure App Service later, move secrets to App Service configuration or Key Vault references.

## Run Locally

From the repository root:

```powershell
dotnet restore backend\Qhse.Api\Qhse.Api.csproj --configfile NuGet.Config
dotnet tool restore
dotnet ef database update --project backend\Qhse.Api\Qhse.Api.csproj --startup-project backend\Qhse.Api\Qhse.Api.csproj
dotnet run --project backend\Qhse.Api\Qhse.Api.csproj
```

Swagger opens at:

```text
http://localhost:5138/swagger
```

## Build

```powershell
dotnet build backend\Qhse.Api\Qhse.Api.csproj
```

## Future JWT Authentication

The API intentionally does not implement authentication yet. The `Auth/` folder contains `ICurrentUserAccessor`, `CurrentUser`, and `HttpCurrentUserAccessor` so JWT claims can be wired later without changing controllers or services.

## Azure Readiness

This backend is ready to evolve toward:

- Azure App Service deployment
- Azure SQL Database
- Azure OpenAI provider implementation in `IAiAnalysisService`
- Azure DevOps build/test/deploy pipeline
- Terraform provisioning later

No Kubernetes, Docker orchestration, Terraform, or microservice complexity is included in this MVP phase.
