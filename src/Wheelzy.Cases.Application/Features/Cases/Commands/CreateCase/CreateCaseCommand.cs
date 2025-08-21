using MediatR;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

public sealed record CreateCaseCommand(string? Note) : IRequest<int>;
