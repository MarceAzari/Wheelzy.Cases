using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Application.Common.Interfaces;
using Wheelzy.Cases.Domain.Entities;
using Wheelzy.Cases.Domain.Enums;
using Wheelzy.Cases.Infrastructure.Persistence;

namespace Wheelzy.Cases.Infrastructure.Services;

public class CaseService : ICaseService
{
    private readonly WheelzyDbContext _db;

    public CaseService(WheelzyDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Crea un caso normalizando datos de referencia y asignando estado inicial
    /// </summary>
    public async Task<int> CreateCaseAsync(short year, string make, string model, string? subModel, string zipCode, int customerId, CancellationToken ct)
    {
        var makeEntity = await _db.Set<Make>().FirstOrDefaultAsync(x => x.Name == make, ct);
        if (makeEntity == null)
        {
            makeEntity = new Make { Name = make };
            _db.Set<Make>().Add(makeEntity);
            await _db.SaveChangesAsync(ct);
        }

        var modelEntity = await _db.Set<Model>().FirstOrDefaultAsync(x => x.Name == model && x.MakeId == makeEntity.MakeId, ct);
        if (modelEntity == null)
        {
            modelEntity = new Model { Name = model, MakeId = makeEntity.MakeId };
            _db.Set<Model>().Add(modelEntity);
            await _db.SaveChangesAsync(ct);
        }

        int? subModelId = null;
        if (!string.IsNullOrEmpty(subModel))
        {
            var subModelEntity = await _db.Set<SubModel>().FirstOrDefaultAsync(x => x.Name == subModel, ct);
            if (subModelEntity == null)
            {
                subModelEntity = new SubModel { Name = subModel };
                _db.Set<SubModel>().Add(subModelEntity);
                await _db.SaveChangesAsync(ct);
            }
            subModelId = subModelEntity.SubModelId;
        }

        var zipCodeEntity = await _db.Set<ZipCode>().FirstOrDefaultAsync(x => x.Code == zipCode, ct);
        if (zipCodeEntity == null)
        {
            zipCodeEntity = new ZipCode { Code = zipCode };
            _db.Set<ZipCode>().Add(zipCodeEntity);
            await _db.SaveChangesAsync(ct);
        }

        var carCase = new CarCase
        {
            Year = year,
            MakeId = makeEntity.MakeId,
            ModelId = modelEntity.ModelId,
            SubModelId = subModelId,
            ZipCodeId = zipCodeEntity.ZipCodeId,
            CustomerId = customerId,
            CreatedAt = DateTime.Now
        };

        _db.Set<CarCase>().Add(carCase);
        await _db.SaveChangesAsync(ct);

        var statusHistory = new CarCaseStatusHistory
        {
            CarCaseId = carCase.CarCaseId,
            StatusId = StatusType.PendingAcceptance,
            StatusDate = DateTime.Now
        };

        _db.Set<CarCaseStatusHistory>().Add(statusHistory);
        await _db.SaveChangesAsync(ct);

        return carCase.CarCaseId;
    }
}