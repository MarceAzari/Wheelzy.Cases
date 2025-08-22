using MediatR;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Features.Cases.Queries.GetCases;

public sealed record GetCasesQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    List<int>? StatusIds,
    int? Year
) : IRequest<List<CaseOverviewDto>>;