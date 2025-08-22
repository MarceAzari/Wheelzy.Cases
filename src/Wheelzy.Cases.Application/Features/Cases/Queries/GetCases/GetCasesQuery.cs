using MediatR;
using Wheelzy.Cases.Application.Common;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Features.Cases.Queries.GetCases;

public sealed record GetCasesQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    int[]? StatusIds,
    int? Year,
    string? Search,
    string? Sort,
    int Page = 1,
    int PageSize = 25
) : IRequest<PagedResult<CaseOverviewDTO>>;