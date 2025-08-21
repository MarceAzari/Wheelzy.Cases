using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Common.Interfaces;

public interface ICaseRepository
{
    Task<List<CaseDto>> GetCasesAsync(DateTime? dateFrom, DateTime? dateTo, List<int>? statusIds, int? year, CancellationToken ct);
}