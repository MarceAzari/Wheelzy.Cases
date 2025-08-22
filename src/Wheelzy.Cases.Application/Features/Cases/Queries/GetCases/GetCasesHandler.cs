using MediatR;
using Wheelzy.Cases.Application.Common.Interfaces;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Features.Cases.Queries.GetCases;

internal sealed class GetCasesHandler : IRequestHandler<GetCasesQuery, List<CaseOverviewDto>>
{
    private readonly ICaseRepository _repository;

    public GetCasesHandler(ICaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CaseOverviewDto>> Handle(GetCasesQuery request, CancellationToken ct)
    {
        return await _repository.GetCasesAsync(request.DateFrom, request.DateTo, request.StatusIds, request.Year, ct);
    }
}