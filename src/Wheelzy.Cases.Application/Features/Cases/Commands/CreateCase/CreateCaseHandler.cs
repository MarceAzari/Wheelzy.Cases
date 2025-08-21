using MediatR;
using Wheelzy.Cases.Domain.Entities;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

internal sealed class CreateCaseHandler : IRequestHandler<CreateCaseCommand, int>
{
    public Task<int> Handle(CreateCaseCommand request, CancellationToken ct)
    {
        // Aquí aplicarías reglas de dominio y persistirías vía repo
        return Task.FromResult(1); // Placeholder ID
    }
}
