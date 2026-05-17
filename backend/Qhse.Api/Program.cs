using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Qhse.Api.Auth;
using Qhse.Api.Data;
using Qhse.Api.Infrastructure.Repositories;
using Qhse.Api.Options;
using Qhse.Api.Services;

var builder = WebApplication.CreateBuilder(args);

const string ReactCorsPolicy = "ReactFrontend";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "QHSE AI Platform API",
        Version = "v1",
        Description = "MVP backend for incidents, audits, non-conformities, risks, reports, and AI-assisted QHSE analysis."
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(ReactCorsPolicy, policy =>
    {
        var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? ["http://localhost:8080", "http://localhost:5173", "http://localhost:3000"];

        policy.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.Configure<AzureOpenAiOptions>(builder.Configuration.GetSection("AzureOpenAI"));

builder.Services.AddDbContext<QhseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QhseDatabase")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserAccessor, HttpCurrentUserAccessor>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<INonConformityService, NonConformityService>();
builder.Services.AddScoped<IRiskService, RiskService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<IAzureOpenAiChatClientFactory, AzureOpenAiChatClientFactory>();
builder.Services.AddScoped<IAiAnalysisService, AiAnalysisService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var logger = context.RequestServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("GlobalExceptionHandler");

        context.Response.ContentType = "application/problem+json";

        if (exception is ArgumentException)
        {
            logger.LogWarning(exception, "Bad request while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await Results.Problem(exception.Message, statusCode: StatusCodes.Status400BadRequest).ExecuteAsync(context);
            return;
        }

        logger.LogError(exception, "Unhandled exception while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await Results.Problem("An unexpected API error occurred.", statusCode: StatusCodes.Status500InternalServerError).ExecuteAsync(context);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ReactCorsPolicy);

// Authentication will be wired here when JWT is introduced.
app.UseAuthorization();

app.MapControllers();

app.Run();
