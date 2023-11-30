using InventoryManager.Application.Mapping.EntityMappings;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;

namespace InventoryManager.Application.Handlers;

public class PriceFileHandler : IFileHandler
{
    private readonly ICsvMapper _csvMapper;
    private readonly IMappingConfigurationsFactory _mappingConfigurationsFactory;
    private readonly PriceClassMap _priceClassMap;

    public PriceFileHandler(ICsvMapper csvMapper, IMappingConfigurationsFactory mappingConfigurationsFactory, PriceClassMap classMap)
    {
        _mappingConfigurationsFactory = mappingConfigurationsFactory;
        _csvMapper = csvMapper;
        _priceClassMap = classMap;
    }
    public async Task HandleAsync(List<string> fileContent)
    {
        var config = _mappingConfigurationsFactory.GetCsvConfiguration(typeof(Price));
        var mappedFileContent = await _csvMapper.MapCsvAsync<Price>(fileContent,config, _priceClassMap);
    }
}