using InventoryManagerAPI.Domain.Csv;
using InventoryManagerAPI.Domain.File;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class MainController : ControllerBase
{
    private readonly IFileUploader  _fileUploadUseCase;
    private readonly ICsvFileReader _csvFileReader;

    public MainController(IFileUploader fileUploadUseCase, ICsvFileReader csvFileReader)
    {
        _fileUploadUseCase = fileUploadUseCase;
        _csvFileReader = csvFileReader;
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

        return Ok("Files uploaded successfully.");
    }
    
    [HttpGet("/products/{SKU}")]
    public async Task<IActionResult> GetProductInformation(string sku)
    {
        return Ok($"Product information for SKU: {sku}");
    }

}