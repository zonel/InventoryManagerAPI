using InventoryManagerAPI.Domain.File;
using Microsoft.AspNetCore.Http;

namespace InventoryManager.Application.File;

public class Upload : IFileUploader
{
    private readonly IFileValidator _fileValidator;
    private readonly IFileSaver _fileSaver;

    public Upload(IFileValidator fileValidator, IFileSaver fileRepository)
    {
        _fileValidator = fileValidator;
        _fileSaver = fileRepository;
    }

    public async Task<Tuple<bool, List<string>>> UploadFilesAsync(List<IFormFile> files, HttpContext context)
    {
        if (!_fileValidator.ValidateFiles(files, context))
            return new Tuple<bool, List<string>>(false, new List<string>());

        var filesPaths = new List<string>();
        
        foreach (var file in files)
        {
            var filePath = await _fileSaver.SaveFileAsync(file);
            filesPaths.Add(filePath);
        }

        return new Tuple<bool, List<string>>(true, filesPaths);
    }
}