namespace InventoryManagerAPI.Domain.Entities;

public class Price
{
    public string? PriceId { get; set; }
    public string? Sku { get; set; }
    public decimal? NettProductPrice { get; set; }
    public decimal? NettProductPriceDiscount { get; set; }
    public decimal? VatRate { get; set; }
    public decimal? NettProductPriceDiscountLogistic { get; set; }
}