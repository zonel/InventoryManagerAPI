using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using InventoryManager.Application.Mapping.TypeConverters;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Mapping.EntityMappings;

public class PriceClassMap : ClassMap<Price>
{
    public PriceClassMap()
    {
        Map(m => m.PriceId).Index(0);
        Map(m => m.Sku).Index(1);
        Map(m => m.NettProductPrice).Index(2);
        Map(m => m.NettProductPriceDiscount).Index(3);
        Map(m => m.VatRate).Index(4).TypeConverter<PercentageNullableDecimalConverter>();
        Map(m => m.NettProductPriceDiscountLogistic).Index(5);
    }
}