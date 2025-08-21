using MediatR;
using FluentValidation;
using Wheelzy.Cases.Domain.Enums;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.UpdateCaseStatus;

internal sealed class UpdateCaseStatusHandler : IRequestHandler<UpdateCaseStatusCommand>
{
    public Task Handle(UpdateCaseStatusCommand request, CancellationToken ct)
    {
        // Validación de negocio: Picked Up requiere StatusDate
        if (request.StatusId == StatusType.PickedUp && request.StatusDate is null)
            throw new ValidationException("Picked Up requiere StatusDate.");

        // Aquí aplicarías la lógica de actualización vía repositorio
        return Task.CompletedTask;
    }
}