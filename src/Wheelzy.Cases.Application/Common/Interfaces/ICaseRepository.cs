using Wheelzy.Cases.Application.Common;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Common.Interfaces;

public interface ICaseRepository
{
    Task<PagedResult<CaseOverviewDTO>> GetCasesAsync(DateTime? dateFrom, DateTime? dateTo, int[]? statusIds, int? year, string? search, string? sort, int page, int pageSize, CancellationToken ct);
    Task<CaseDetailDTO?> GetByIdAsync(int caseId, CancellationToken ct);
}