namespace Wheelzy.Cases.Infrastructure.Persistence.Models;

public class CaseOverview
{
    public int CarCaseId { get; set; }
    public int Year { get; set; }
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string? SubModel { get; set; }
    public string Code { get; set; } = null!;
    public string CurrentBuyer { get; set; } = null!;
    public decimal CurrentQuote { get; set; }
    public string CurrentStatus { get; set; } = null!;
    public DateTime StatusDate { get; set; }
}