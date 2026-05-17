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
        Description = "QHSE AI Backend API"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(ReactCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<AzureOpenAiOptions>(
    builder.Configuration.GetSection("AzureOpenAI"));

builder.Services.AddDbContext<QhseDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("QhseDatabase")));

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

        context.Response.ContentType = "application/problem+json";

        if (exception is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await Results.Problem(
                exception.Message,
                statusCode: StatusCodes.Status400BadRequest)
                .ExecuteAsync(context);

            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await Results.Problem(
            "Unexpected API error.",
            statusCode: StatusCodes.Status500InternalServerError)
            .ExecuteAsync(context);
    });
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(ReactCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();