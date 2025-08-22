namespace Wheelzy.Cases.Domain.Entities;

/// <summary>
/// Cotización de comprador por código postal
/// </summary>
public class BuyerZipQuote
{
    public int BuyerZipQuoteId { get; set; }



    public int BuyerId { get; set; }



    public int ZipCodeId { get; set; }



    public decimal Amount { get; set; }
}