namespace Wheelzy.Cases.Application.Features.Cases.Dtos;

public sealed record CaseDetailDto(
    int CaseId,
    int CustomerId,
    short Year,
    int MakeId,
    int ModelId,
    int? SubModelId,
    int ZipCodeId,
    DateTime CreatedAt,
    string MakeName,
    string ModelName,
    string? SubModelName,
    string Zip,
    string? CurrentBuyer,
    decimal? CurrentQuote,
    int? CurrentStatusId,
    string? CurrentStatusName,
    DateTime? CurrentStatusDate
);