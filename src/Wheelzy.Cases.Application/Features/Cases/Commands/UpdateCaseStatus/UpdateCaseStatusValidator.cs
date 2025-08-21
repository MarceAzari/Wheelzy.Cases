using FluentValidation;
using Wheelzy.Cases.Domain.Enums;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.UpdateCaseStatus;

public sealed class UpdateCaseStatusValidator : AbstractValidator<UpdateCaseStatusCommand>
{
    public UpdateCaseStatusValidator()
    {
        RuleFor(x => x.CarCaseId).GreaterThan(0);
        RuleFor(x => x.StatusId).GreaterThan(0);
        
        // Regla de negocio: Picked Up requiere StatusDate
        RuleFor(x => x.StatusDate)
            .NotNull()
            .When(x => x.StatusId == StatusType.PickedUp)
            .WithMessage("Picked Up requiere StatusDate.");
    }
}