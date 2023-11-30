using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManagerAPI.Domain.Configuration;
using InventoryManagerAPI.Domain.DatabaseInteraction;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Handlers;

public class PriceFileHandler : IFileHandler
{
    private readonly ICsvMapper _csvMapper;
    private readonly IMappingConfigurationsFactory _mappingConfigurationsFactory;
    private readonly PriceClassMap _priceClassMap;
    private readonly IDatabaseBulkInsert _databaseBulkInsert;
    private readonly IConfigurationManager _configurationManager;


    public PriceFileHandler(
        ICsvMapper csvMapper, 
        IMappingConfigurationsFactory mappingConfigurationsFactory, 
        PriceClassMap classMap,
        IDatabaseBulkInsert databaseBulkInsert,
        IConfigurationManager configuration)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
        _csvMapper = csvMapper;
        _priceClassMap = classMap;
        _databaseBulkInsert = databaseBulkInsert;
        _configurationManager = configuration;
    }
    public async Task HandleAsync(List<string> fileContent)
    {
        var connectionString = _configurationManager.GetConnectionString();
        var config = _mappingConfigurationsFactory.GetCsvConfiguration(typeof(Price));
        var mappedFileContent = await _csvMapper.MapCsvAsync<Price>(fileContent,config, _priceClassMap);
        await _databaseBulkInsert.InsertDataAsync(connectionString, "Prices", mappedFileContent);
    }
}