using System.Data.SqlClient;
using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManagerAPI.Domain.Configuration;
using InventoryManagerAPI.Domain.DatabaseInteraction;
using InventoryManagerAPI.Domain.Entities;
using InventoryManagerAPI.Domain.Filtering;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using Z.Dapper.Plus;



namespace InventoryManager.Application.Handlers;

public class InventoryFileHandler : IFileHandler
{
    private readonly ICsvMapper _csvMapper;
    private readonly IMappingConfigurationsFactory _mappingConfigurationsFactory;
    private readonly InventoryClassMap _inventoryClassMap;
    private readonly IFilterEntity _queryFilter;
    private readonly IDatabaseBulkInsert _databaseBulkInsert;
    private readonly IConfigurationManager _configurationManager;


    public InventoryFileHandler(ICsvMapper csvMapper, 
        IMappingConfigurationsFactory mappingConfigurationsFactory, 
        InventoryClassMap classMap, 
        IFilterEntity queryFilter,
        IDatabaseBulkInsert databaseBulkInsert,
        IConfigurationManager configuration)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
        _csvMapper = csvMapper;
        _inventoryClassMap = classMap;
        _queryFilter = queryFilter;
        _databaseBulkInsert = databaseBulkInsert;
        _configurationManager = configuration;
    }
    public async Task HandleAsync(List<string> fileContent)
    {
        var connectionString = _configurationManager.GetConnectionString();
        var config = _mappingConfigurationsFactory.GetCsvConfiguration(typeof(Inventory));
        var mappedFileContent = await _csvMapper.MapCsvAsync<Inventory>(fileContent,config, _inventoryClassMap);
        var filteredFileContent = await _queryFilter.FilterEntitiesAsync<Inventory>(mappedFileContent, x => x.Shipping == "24h");
        await _databaseBulkInsert.InsertDataAsync(connectionString, "Inventory", filteredFileContent);
    }
}