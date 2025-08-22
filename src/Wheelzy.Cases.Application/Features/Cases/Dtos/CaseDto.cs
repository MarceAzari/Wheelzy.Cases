namespace Wheelzy.Cases.Application.Features.Cases.Dtos;

public sealed record CaseDto(
    int CarCaseId,
    int CustomerId,
    int Year,
    int MakeId,
    int ModelId,
    int? SubModelId,
    int ZipCodeId,
    DateTime CreatedAt,
    int? CurrentStatusId,
    string? CurrentStatusName
);