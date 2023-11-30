using CsvHelper.Configuration;

namespace InventoryManagerAPI.Domain.Mapping;

public interface IMappingConfigurationsFactory
{
    public CsvConfiguration GetCsvConfiguration(object type);
}