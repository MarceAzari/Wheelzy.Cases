namespace Wheelzy.Cases.Domain.Entities;

public class BuyerZipQuote
{
    public int BuyerZipQuoteId { get; set; }
    public int BuyerId { get; set; }
    public int ZipCodeId { get; set; }
    public decimal Amount { get; set; }
}