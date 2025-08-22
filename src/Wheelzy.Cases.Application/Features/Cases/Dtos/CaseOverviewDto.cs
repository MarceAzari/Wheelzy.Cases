namespace Wheelzy.Cases.Application.Features.Cases.Dtos;

public record CaseOverviewDto(
    int CaseId, 
    short Year, 
    string Make, 
    string Model, 
    string? SubModel,
    string Zip, 
    string? CurrentBuyer, 
    decimal? CurrentQuote,
    int? CurrentStatusId, 
    string? CurrentStatus, 
    DateTime? CurrentStatusDate
);