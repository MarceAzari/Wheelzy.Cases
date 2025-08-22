using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Application.Common.Interfaces;
using Wheelzy.Cases.Application.Features.Cases.Dtos;
using Wheelzy.Cases.Domain.Entities;
using Wheelzy.Cases.Infrastructure.Persistence;
using Wheelzy.Cases.Infrastructure.Persistence.Models;

namespace Wheelzy.Cases.Infrastructure.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly WheelzyDbContext _db;

    public CaseRepository(WheelzyDbContext db)
    {
        _db = db;
    }

    public async Task<List<CaseOverviewDto>> GetCasesAsync(DateTime? dateFrom, DateTime? dateTo, List<int>? statusIds, int? year, CancellationToken ct)
    {
        IQueryable<CaseOverview> q = _db.CaseOverview.AsNoTracking();

        if (year.HasValue)
            q = q.Where(x => x.Year == year.Value);

        if (dateFrom.HasValue)
            q = q.Where(x => x.CurrentStatusDate >= dateFrom.Value);

        if (dateTo.HasValue)
            q = q.Where(x => x.CurrentStatusDate <= dateTo.Value);

        if (statusIds is { Count: > 0 })
            q = q.Where(x => x.CurrentStatusId.HasValue && statusIds.Contains(x.CurrentStatusId.Value));

        return await q.Select(x => new CaseOverviewDto(
                x.CaseId, x.Year, x.Make, x.Model, x.SubModel, x.Zip,
                x.CurrentBuyer, x.CurrentQuote, x.CurrentStatusId,
                x.CurrentStatus, x.CurrentStatusDate
            ))
            .ToListAsync(ct);
    }

    public async Task<CaseDetailDto?> GetByIdAsync(int caseId, CancellationToken ct)
    {
        var q = _db.Set<CarCase>()
            .AsNoTracking()
            .Where(c => c.CarCaseId == caseId)
            .Select(c => new
            {
                c.CarCaseId,
                c.CustomerId,
                c.Year,
                c.MakeId,
                c.ModelId,
                c.SubModelId,
                c.ZipCodeId,
                c.CreatedAt,
                MakeName = _db.Set<Make>().Where(m => m.MakeId == c.MakeId).Select(m => m.Name).FirstOrDefault(),
                ModelName = _db.Set<Model>().Where(mo => mo.ModelId == c.ModelId).Select(mo => mo.Name).FirstOrDefault(),
                SubModelName = _db.Set<SubModel>().Where(sm => sm.SubModelId == c.SubModelId).Select(sm => sm.Name).FirstOrDefault(),
                Zip = _db.Set<ZipCode>().Where(z => z.ZipCodeId == c.ZipCodeId).Select(z => z.Code).FirstOrDefault(),
                CurrentQuote = _db.Set<CarCaseQuote>()
                                  .Where(q => q.CarCaseId == c.CarCaseId && q.IsCurrent)
                                  .Select(q => new { q.Amount, q.BuyerId })
                                  .FirstOrDefault(),
                LastStatus = _db.Set<CarCaseStatusHistory>()
                               .Where(h => h.CarCaseId == c.CarCaseId)
                               .OrderByDescending(h => h.StatusDate)
                               .ThenByDescending(h => h.CarCaseStatusHistoryId)
                               .Select(h => new { h.StatusId, h.StatusDate })
                               .FirstOrDefault()
            });

        var row = await q.FirstOrDefaultAsync(ct);
        if (row is null) return null;

        var buyerName = row.CurrentQuote == null
            ? null
            : await _db.Set<Buyer>()
                .Where(b => b.BuyerId == row.CurrentQuote.BuyerId)
                .Select(b => b.Name)
                .FirstOrDefaultAsync(ct);

        var statusName = row.LastStatus == null
            ? null
            : await _db.Set<Status>()
                .Where(s => s.StatusId == row.LastStatus.StatusId)
                .Select(s => s.Name)
                .FirstOrDefaultAsync(ct);

        return new CaseDetailDto(
            row.CarCaseId,
            row.CustomerId,
            row.Year,
            row.MakeId,
            row.ModelId,
            row.SubModelId,
            row.ZipCodeId,
            row.CreatedAt,
            row.MakeName ?? "",
            row.ModelName ?? "",
            row.SubModelName,
            row.Zip ?? "",
            buyerName,
            row.CurrentQuote?.Amount,
            row.LastStatus?.StatusId,
            statusName,
            row.LastStatus?.StatusDate
        );
    }
}