using MediatR;
using Wheelzy.Cases.Application.Common.Interfaces;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Features.Cases.Queries.GetCaseById;

public sealed class GetCaseByIdHandler : IRequestHandler<GetCaseByIdQuery, CaseDetailDto?>
{
    private readonly ICaseRepository _repository;

    public GetCaseByIdHandler(ICaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<CaseDetailDto?> Handle(GetCaseByIdQuery request, CancellationToken ct)
    {
        return await _repository.GetByIdAsync(request.CaseId, ct);
    }
}