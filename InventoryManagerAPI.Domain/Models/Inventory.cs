namespace InventoryManagerAPI.Domain.Models;

public class Inventory
{
    public int Product_ID { get; set; }
    public string SKU { get; set; }
    public string Unit { get; set; }
    public int Qty { get; set; }
    public string Manufacturer { get; set; }
    public int Shipping { get; set; }
    public decimal Shipping_Cost { get; set; }
}