using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManagerAPI.Domain.Configuration;
using InventoryManagerAPI.Domain.DatabaseInteraction;
using InventoryManagerAPI.Domain.Filtering;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Handlers;

public class ProductFileHandler : IFileHandler
{
    private readonly ICsvMapper _csvMapper;
    private readonly IMappingConfigurationsFactory _mappingConfigurationsFactory;
    private readonly ProductClassMap _productClassMap;
    private readonly IFilterEntity _queryFilter;
    private readonly IDatabaseBulkInsert _databaseBulkInsert;
    private readonly IConfigurationManager _configurationManager;

    public ProductFileHandler(
        ICsvMapper csvMapper, 
        IMappingConfigurationsFactory mappingConfigurationsFactory, 
        ProductClassMap classMap, 
        IFilterEntity queryFilter,
        IDatabaseBulkInsert databaseBulkInsert,
        IConfigurationManager configuration)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
        _csvMapper = csvMapper;
        _productClassMap = classMap;
        _queryFilter = queryFilter;
        _databaseBulkInsert = databaseBulkInsert;
        _configurationManager = configuration;
    }
    public async Task HandleAsync(List<string> fileContent)
    {
        var ConnectionString = _configurationManager.GetConnectionString();
        var config = _mappingConfigurationsFactory.GetCsvConfiguration(typeof(Product));
        var mappedFileContent = await _csvMapper.MapCsvAsync<Product>(fileContent,config, _productClassMap);
        var filteredFileContent = await _queryFilter.FilterEntitiesAsync<Product>(mappedFileContent, x => x.IsWire == false && x.Shipping == "24h");
        await _databaseBulkInsert.InsertDataAsync(ConnectionString, "Products", filteredFileContent);
    }
}