using MediatR;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

public sealed record CreateCaseCommand(
    short Year,
    string Make,
    string Model,
    string? SubModel,
    string ZipCode,
    int CustomerId
) : IRequest<int>;
