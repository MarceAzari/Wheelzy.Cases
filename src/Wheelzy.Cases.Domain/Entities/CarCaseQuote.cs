namespace Wheelzy.Cases.Domain.Entities;

public class CarCaseQuote
{
    public int CarCaseQuoteId { get; set; }


    public int CarCaseId { get; set; }


    public int BuyerId { get; set; }


    public decimal Amount { get; set; }


    public bool IsCurrent { get; set; }


    public DateTime CreatedAt { get; set; }
}