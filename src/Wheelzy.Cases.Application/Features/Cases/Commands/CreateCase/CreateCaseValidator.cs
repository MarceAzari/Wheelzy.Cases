using FluentValidation;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

public sealed class CreateCaseValidator : AbstractValidator<CreateCaseCommand>
{
    public CreateCaseValidator()
    {
        RuleFor(x => x.Year).GreaterThan((short)1900).LessThanOrEqualTo((short)DateTime.Now.Year);
        RuleFor(x => x.Make).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Model).NotEmpty().MaximumLength(50);
        RuleFor(x => x.SubModel).MaximumLength(50);
        RuleFor(x => x.ZipCode).NotEmpty().MaximumLength(10);
        RuleFor(x => x.CustomerId).GreaterThan(0);
    }
}