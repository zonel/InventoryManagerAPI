using InventoryManager.Application.Handlers;
using InventoryManagerAPI.Domain.Csv;
using InventoryManagerAPI.Domain.File;
using InventoryManagerAPI.Domain.Handler;
using InventoryManagerAPI.Domain.Mapping;
using InventoryManagerAPI.Domain.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class MainController : ControllerBase
{
    private readonly IFileUploader  _fileUploadUseCase;
    private readonly ICsvFileReader _csvFileReader;
    private readonly ICsvMapper _csvMapper;
    private readonly IFileHandlerFactory _fileHandlerFactory;

    public MainController(IFileUploader fileUploadUseCase, ICsvFileReader csvFileReader, ICsvMapper csvMapper, IFileHandlerFactory fileHandlerFactory)
    {
        _fileUploadUseCase = fileUploadUseCase;
        _csvFileReader = csvFileReader;
        _csvMapper = csvMapper;
        _fileHandlerFactory = fileHandlerFactory;
    }

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
                Console.WriteLine($"Unrecognized file: {filePath}");
            }
            // Perform actions common to all files if needed...
        });

        await Task.WhenAll(tasks);
        
        return Ok("Files uploaded successfully.");
    }
    
    [HttpGet("/products/{SKU}")]
    public async Task<IActionResult> GetProductInformation(string sku)
    {
        return Ok($"Product information for SKU: {sku}");
    }

}