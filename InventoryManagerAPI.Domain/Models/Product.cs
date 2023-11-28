namespace InventoryManagerAPI.Domain.Models;

public class Product
{
    public int ID { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public string EAN { get; set; }
    public string Producer_Name { get; set; }
    public string Category { get; set; }
    public bool Is_Wire { get; set; }
    public bool Available { get; set; }
    public bool Is_Vendor { get; set; }
    public string Default_Image { get; set; }
}