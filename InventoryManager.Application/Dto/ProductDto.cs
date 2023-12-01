namespace InventoryManager.Application.Dto;

public record ProductDto
{
    public string ProductName { get; set; }
    public string EAN { get; set; }
    public string ProducerName { get; set; }
    public string Category { get; set; }
    public string ImageURL { get; set; }
    public int StockQuantity { get; set; }
    public string LogisticUnit { get; set; }
    public decimal NetPurchasePrice { get; set; }
    public decimal DeliveryCost { get; set; }
}