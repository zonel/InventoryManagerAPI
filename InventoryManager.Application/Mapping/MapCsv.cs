using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManagerAPI.Domain.Exceptions;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;
using Serilog;
using Z.BulkOperations;

namespace InventoryManager.Application.Mapping;

public class MapCsv : ICsvMapper
{
    private readonly IMappingConfigurationsFactory _mappingConfigurationsFactory;

    public MapCsv(IMappingConfigurationsFactory mappingConfigurationsFactory)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
    }
    public async Task<List<T>> MapCsvAsync<T>(List<string> csvLines, CsvConfiguration config, ClassMap<T> classMap)
    {
        try
        {
            using (var reader = new StringReader(string.Join(Environment.NewLine, csvLines)))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap(classMap.GetType()); 
                var records = csv.GetRecords<T>().ToList();
                
                return records;
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error mapping CSV: {ex.Message}");
            throw new MappingException($"Error mapping CSV: {ex.Message}");
        }
    }
}