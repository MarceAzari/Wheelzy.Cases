using MediatR;

namespace Wheelzy.Cases.Application.Features.Cases.Commands.UpdateCaseStatus;

public sealed record UpdateCaseStatusCommand(int CarCaseId, int StatusId, DateTime? StatusDate) : IRequest;