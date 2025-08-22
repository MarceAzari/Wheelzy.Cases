namespace Wheelzy.Cases.Domain.Entities;

public class CarCase
{
    public int CarCaseId { get; set; }
    public int CustomerId { get; set; }
    public short Year { get; set; }
    public int MakeId { get; set; }
    public int ModelId { get; set; }
    public int? SubModelId { get; set; }
    public int ZipCodeId { get; set; }
    public DateTime CreatedAt { get; set; }
}
