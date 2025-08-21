namespace Wheelzy.Cases.Domain.Entities;

public class CarCase
{
    public int CarCaseId { get; set; }
    public int Year { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string SubModel { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CurrentBuyer { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string CurrentStatus { get; set; } = string.Empty;
    public DateTime StatusDate { get; set; }
}
