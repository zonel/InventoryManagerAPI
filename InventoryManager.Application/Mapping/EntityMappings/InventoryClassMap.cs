using CsvHelper.Configuration;
using InventoryManager.Application.Mapping.TypeConverters;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Mapping.EntityMappings;

public class InventoryClassMap : ClassMap<Inventory>
{
    public InventoryClassMap()
    {
        Map(m => m.ProductId).Index(0).TypeConverter<NullableIntConverter>();
        Map(m => m.Sku).Index(1);
        Map(m => m.Unit).Index(2);
        Map(m => m.Qty).Index(3);
        Map(m => m.Manufacturer).Index(4);
        Map(m => m.Shipping).Index(6).Default(""); 
        Map(m => m.ShippingCost).Index(7).Default(""); 
    }
}