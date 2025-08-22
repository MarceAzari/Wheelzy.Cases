using MediatR;
using Wheelzy.Cases.Application.Features.Cases.Dtos;

namespace Wheelzy.Cases.Application.Features.Cases.Queries.GetCaseById;

public sealed record GetCaseByIdQuery(int CaseId) : IRequest<CaseDetailDTO?>;