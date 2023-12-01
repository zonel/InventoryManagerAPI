using CsvHelper.Configuration;
using InventoryManager.Application.Mapping.TypeConverters;
using InventoryManagerAPI.Domain.Entities;

namespace InventoryManager.Application.Mapping.EntityMappings;

public class ProductClassMap : ClassMap<Product>
{
    public ProductClassMap()
    {
        Map(m => m.Id).Index(0).TypeConverter<NullableIntConverter>();
        Map(m => m.Sku).Index(1);
        Map(m => m.Name).Index(2);
        Map(m => m.Ean).Index(4);
        Map(m => m.ProducerName).Index(6);
        Map(m => m.Category).Index(7);
        Map(m => m.IsWire).Index(8);
        Map(m => m.Shipping).Index(9);
        Map(m => m.Available).Index(11);
        Map(m => m.IsVendor).Index(16);
        Map(m => m.DefaultImage).Index(18);
    }
}