using Qhse.Api.Contracts.Reports;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Infrastructure.Repositories;

namespace Qhse.Api.Services;

public sealed class ReportService(IRepository<Report> repository) : IReportService
{
    public async Task<IReadOnlyList<ReportDto>> GetAllAsync(CancellationToken cancellationToken) => (await repository.ListAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<ReportDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ReportDto> CreateAsync(CreateReportRequest request, CancellationToken cancellationToken)
    {
        var entity = new Report
        {
            Reference = ReferenceGenerator.New("RPT"),
            Name = request.Name,
            Type = request.Type,
            UploadedBy = request.UploadedBy,
            UploadedAt = request.UploadedAt,
            SizeKb = request.SizeKb,
            BlobUri = request.BlobUri
        };

        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<ReportDto?> UpdateAsync(Guid id, UpdateReportRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return null;

        entity.Name = request.Name;
        entity.Type = request.Type;
        entity.UploadedBy = request.UploadedBy;
        entity.UploadedAt = request.UploadedAt;
        entity.SizeKb = request.SizeKb;
        entity.BlobUri = request.BlobUri;
        repository.Update(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        repository.Delete(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ReportDto ToDto(Report entity) => new(entity.Id, entity.Reference, entity.Name, entity.Type, entity.UploadedBy, entity.UploadedAt, entity.SizeKb, entity.BlobUri);
}
