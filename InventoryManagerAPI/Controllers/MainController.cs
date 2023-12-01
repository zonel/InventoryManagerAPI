using InventoryManager.Application.Dto;
using InventoryManagerAPI.Domain.Csv;
using InventoryManagerAPI.Domain.Exceptions;
using InventoryManagerAPI.Domain.File;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InventoryManagerAPI.Controllers;

/// <summary>
/// Main controller of the API.
/// </summary>
[ApiController]
[Route("[controller]")]

public class MainController : ControllerBase
{
    private readonly IFileUploader  _fileUploadUseCase;
    private readonly ICsvFileReader _csvFileReader;
    private readonly IFileHandlerFactory _fileHandlerFactory;
    private readonly IDatabaseRepository _databaseRepository;

    /// <summary>
    /// Dependency injection constructor.
    /// </summary>
    /// <param name="fileUploadUseCase">It handles process of uploading files.</param>
    /// <param name="csvFileReader">It handles process of reading CSV files.</param>
    /// <param name="fileHandlerFactory">This factory creates appropriate fileHandler for known files.</param>
    /// <param name="databaseRepository">This repository handles interactions with databse.</param>
    public MainController(
        IFileUploader fileUploadUseCase, 
        ICsvFileReader csvFileReader, 
        IFileHandlerFactory fileHandlerFactory,
        IDatabaseRepository databaseRepository)
    {
        _fileUploadUseCase = fileUploadUseCase;
        _csvFileReader = csvFileReader;
        _fileHandlerFactory = fileHandlerFactory;
        _databaseRepository = databaseRepository;
    }

    /// <summary>
    /// This endpoint imports data from the provided files then makes operations on them and finally it stores selected ones in database.
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <exception cref="UnrecognizedFileException">File title not recognized / No matching handler found</exception>
    [HttpPost("/importData")]
    public async Task<IActionResult> ImportData()
    {
        var uploadFileSizeLimitInMb = 500;
        var formOptions = new FormOptions { MultipartBodyLengthLimit = uploadFileSizeLimitInMb * 1024 * 1024 };
        var form = await Request.ReadFormAsync(formOptions);
        var files = form.Files.ToList();

        var result = await _fileUploadUseCase.UploadFilesAsync(files, HttpContext);
        if (!result.Item1)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        
        var tasks = result.Item2.Select(async filePath =>
        {
            var handler = _fileHandlerFactory.GetFileHandler(filePath);

            if (handler != null)
            {
                var fileContent = await _csvFileReader.ReadFileAsync(filePath);
                await handler.HandleAsync(fileContent.ToList());
            }
            else
            {
                // Handle unrecognized files or paths
                Log.Error($"Unrecognized file: {filePath}");
                throw new UnrecognizedFileException($"Unrecognized file: {filePath}");
            }
        });

        await Task.WhenAll(tasks);
        
        return Ok("[200] - Operations on files you've provided were successful.");
    }
    
    /// <summary>
    /// This endpoint returns a productDto object of the provided sku.
    /// </summary>
    /// <param name="sku">SKU of item searched</param>
    /// <returns>ProductDto</returns>
    [HttpGet("/products/{sku}")]
    public async Task<IActionResult> GetProductInformation([FromRoute]string sku)
    {
            var productInfo = await _databaseRepository.GetProductDataAsync<ProductDto>(sku);
            if (productInfo == null)
                return NotFound();
            return Ok(productInfo);
        }
    }