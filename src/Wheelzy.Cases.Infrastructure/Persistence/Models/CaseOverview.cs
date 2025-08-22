namespace Wheelzy.Cases.Infrastructure.Persistence.Models;

public sealed class CaseOverview
{
    public int CaseId { get; set; }


    public short Year { get; set; }


    public string Make { get; set; } = "";
    public string Model { get; set; } = "";
    public string? SubModel { get; set; }


    public string Zip { get; set; } = "";
    public string? CurrentBuyer { get; set; }


    public decimal? CurrentQuote { get; set; }


    public string? CurrentStatus { get; set; }


    public DateTime? CurrentStatusDate { get; set; }


    public int? CurrentStatusId { get; set; }
}