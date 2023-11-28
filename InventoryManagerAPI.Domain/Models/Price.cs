namespace InventoryManagerAPI.Domain.Models;

public class Price
{
    public int Price_ID { get; set; }
    public string SKU { get; set; }
    public decimal Nett_Product_Price { get; set; }
    public decimal Nett_Product_Price_Discount { get; set; }
    public decimal VAT_Rate { get; set; }
    public decimal Nett_Product_Price_Discount_Logistic { get; set; }
}