using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Application.Common.Extensions;
using Wheelzy.Cases.Application.Common.Interfaces;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Infrastructure.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly WheelzyDbContext _db;

    public CaseRepository(WheelzyDbContext db)
    {
        _db = db;
    }

    public async Task<List<CaseDto>> GetCasesAsync(DateTime? dateFrom, DateTime? dateTo, List<int>? statusIds, int? year, CancellationToken ct)
    {
        var q = _db.CarCases
            .AsNoTracking()
            .Select(x => new CaseDto(
                x.CarCaseId,
                x.Year,
                x.Make,
                x.Model,
                x.CurrentStatus,
                x.StatusDate
            ));

        q = q.WhereIf(dateFrom != null, s => s.Where(x => x.CurrentStatusDate >= dateFrom!.Value));
        q = q.WhereIf(dateTo != null, s => s.Where(x => x.CurrentStatusDate < dateTo!.Value.AddDays(1)));
        q = q.WhereIf(statusIds is { Count: > 0 }, s => s.Where(x => statusIds!.Any(id => x.Id.ToString().Contains(id.ToString()))));
        q = q.WhereIf(year != null, s => s.Where(x => x.Year == year));

        return await q.ToListAsync(ct);
    }
}