using FluentValidation;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

public sealed class CreateCaseValidator : AbstractValidator<CreateCaseCommand>
{
    public CreateCaseValidator()
    {
        // Reglas mínimas de ejemplo
        // RuleFor(x => x.Note).MaximumLength(200);
    }
}
