using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Common.Interfaces;

public interface ICaseRepository
{
    Task<List<CaseOverviewDto>> GetCasesAsync(DateTime? dateFrom, DateTime? dateTo, List<int>? statusIds, int? year, CancellationToken ct);
    Task<CaseDetailDto?> GetByIdAsync(int caseId, CancellationToken ct);
}