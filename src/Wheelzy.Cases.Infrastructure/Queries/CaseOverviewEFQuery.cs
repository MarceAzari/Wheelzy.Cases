using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Domain.Entities;
using Wheelzy.Cases.Infrastructure.Persistence;

namespace Wheelzy.Cases.Infrastructure.Queries;

/// <summary>
/// Consulta EF equivalente a la consulta SQL del challenge
/// </summary>
public class CaseOverviewEFQuery
{
    private readonly WheelzyDbContext _db;

    public CaseOverviewEFQuery(WheelzyDbContext db)
    {
        _db = db;
    }




    public async Task<List<CaseOverviewResult>> GetCaseOverviewAsync(CancellationToken ct = default)
    {
        var query = from c in _db.Set<CarCase>()
                    join m in _db.Set<Make>() on c.MakeId equals m.MakeId
                    join mo in _db.Set<Model>() on c.ModelId equals mo.ModelId
                    join z in _db.Set<ZipCode>() on c.ZipCodeId equals z.ZipCodeId
                    join sm in _db.Set<SubModel>() on c.SubModelId equals sm.SubModelId into subModels
                    from sm in subModels.DefaultIfEmpty()
                    join ccq in _db.Set<CarCaseQuote>().Where(x => x.IsCurrent) on c.CarCaseId equals ccq.CarCaseId into quotes
                    from ccq in quotes.DefaultIfEmpty()
                    join b in _db.Set<Buyer>() on ccq.BuyerId equals b.BuyerId into buyers
                    from b in buyers.DefaultIfEmpty()
                    join latestStatus in (
                        from csh in _db.Set<CarCaseStatusHistory>()
                        group csh by csh.CarCaseId into g
                        select new
                        {
                            CarCaseId = g.Key,
                            StatusId = g.OrderByDescending(x => x.StatusDate)
                                       .ThenByDescending(x => x.CarCaseStatusHistoryId)
                                       .First().StatusId,
                            StatusDate = g.OrderByDescending(x => x.StatusDate)
                                        .ThenByDescending(x => x.CarCaseStatusHistoryId)
                                        .First().StatusDate
                        }
                    ) on c.CarCaseId equals latestStatus.CarCaseId into statuses
                    from latestStatus in statuses.DefaultIfEmpty()
                    join s in _db.Set<Status>() on latestStatus.StatusId equals s.StatusId into statusNames
                    from s in statusNames.DefaultIfEmpty()
                    select new CaseOverviewResult
                    {
                        CarCaseId = c.CarCaseId,
                        Year = c.Year,
                        Make = m.Name,
                        Model = mo.Name,
                        SubModel = sm != null ? sm.Name : null,
                        ZipCode = z.Code,
                        CurrentBuyer = b != null ? b.Name : null,
                        CurrentQuote = ccq != null ? ccq.Amount : null,
                        CurrentStatus = s != null ? s.Name : null,
                        CurrentStatusDate = latestStatus != null ? latestStatus.StatusDate : null
                    };

        return await query.AsNoTracking().ToListAsync(ct);
    }
}




public class CaseOverviewResult
{
    public int CarCaseId { get; set; }



    public short Year { get; set; }



    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? SubModel { get; set; }



    public string ZipCode { get; set; } = string.Empty;
    public string? CurrentBuyer { get; set; }



    public decimal? CurrentQuote { get; set; }



    public string? CurrentStatus { get; set; }



    public DateTime? CurrentStatusDate { get; set; }
}