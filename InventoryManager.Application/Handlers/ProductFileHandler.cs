using InventoryManager.Application.Mapping.EntityMappings;
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

    public ProductFileHandler(ICsvMapper csvMapper, IMappingConfigurationsFactory mappingConfigurationsFactory, ProductClassMap classMap, IFilterEntity queryFilter)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
        _csvMapper = csvMapper;
        _productClassMap = classMap;
        _queryFilter = queryFilter;
    }
    public async Task HandleAsync(List<string> fileContent)
    {
        var config = _mappingConfigurationsFactory.GetCsvConfiguration(typeof(Product));
        var mappedFileContent = await _csvMapper.MapCsvAsync<Product>(fileContent,config, _productClassMap);
        var filteredFileContent = _queryFilter.FilterEntities<Product>(mappedFileContent, x => x.IsWire == false && x.Shipping == "24h");
    }
}