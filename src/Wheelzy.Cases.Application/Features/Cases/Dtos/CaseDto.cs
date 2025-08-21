namespace Wheelzy.Cases.Application.Features.Cases.Dtos;

public sealed record CaseDto(int Id, int Year, string Make, string Model, string CurrentStatus, DateTime CurrentStatusDate);