using MediatR;
using Wheelzy.Cases.Application.Common.Interfaces;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

internal sealed class CreateCaseHandler : IRequestHandler<CreateCaseCommand, int>
{
    private readonly ICaseService _caseService;

    public CreateCaseHandler(ICaseService caseService)
    {
        _caseService = caseService;
    }



    public async Task<int> HandleAsync(CreateCaseCommand request, CancellationToken ct)
    {
        return await _caseService.CreateCaseAsync(
            request.Year, 
            request.Make, 
            request.Model, 
            request.SubModel, 
            request.ZipCode, 
            request.CustomerId, 
            ct);
    }
}