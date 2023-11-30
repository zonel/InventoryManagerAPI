using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManagerAPI.Domain.Filtering;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Handlers;

public class InventoryFileHandler : IFileHandler
{
    private readonly ICsvMapper _csvMapper;
    private readonly IMappingConfigurationsFactory _mappingConfigurationsFactory;
    private readonly InventoryClassMap _inventoryClassMap;
    private readonly IFilterEntity _queryFilter;

    public InventoryFileHandler(ICsvMapper csvMapper, IMappingConfigurationsFactory mappingConfigurationsFactory, InventoryClassMap classMap, IFilterEntity queryFilter)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
        _csvMapper = csvMapper;
        _inventoryClassMap = classMap;
        _queryFilter = queryFilter;
    }
    public async Task HandleAsync(List<string> fileContent)
    {
        var config = _mappingConfigurationsFactory.GetCsvConfiguration(typeof(Inventory));
        var mappedFileContent = await _csvMapper.MapCsvAsync<Inventory>(fileContent,config, _inventoryClassMap);
        var filteredInventory = _queryFilter.FilterEntities<Inventory>(mappedFileContent, x => x.Shipping == "24h");
    }
    

}