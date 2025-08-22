namespace Wheelzy.Cases.Domain.Entities;

public class CarCaseStatusHistory
{
    public int CarCaseStatusHistoryId { get; set; }



    public int CarCaseId { get; set; }



    public int StatusId { get; set; }



    public DateTime StatusDate { get; set; }
}