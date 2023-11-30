using System.Globalization;
using CsvHelper.Configuration;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Mapping.MappingConfigurations;

public class MappingConfigurationsFactory : IMappingConfigurationsFactory
{
    public CsvConfiguration GetCsvConfiguration(object type)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            MissingFieldFound = null,
            HasHeaderRecord = true,
            BadDataFound = null
        };


        if (type == typeof(Product))
        {
            config.Delimiter = ";";
        }
        
        if (type == typeof(Price))
        {
            config.HasHeaderRecord = false;
        }

        return config;
    }

}