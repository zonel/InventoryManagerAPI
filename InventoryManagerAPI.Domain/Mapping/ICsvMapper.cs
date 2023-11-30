using CsvHelper.Configuration;

namespace InventoryManagerAPI.Domain.Mapping;

public interface ICsvMapper
{
    public Task<List<T>> MapCsvAsync<T>(List<string> csvLines, CsvConfiguration config, ClassMap<T> classMap);
}