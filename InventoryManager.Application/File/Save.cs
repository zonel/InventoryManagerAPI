using InventoryManagerAPI.Domain.File;
using Microsoft.AspNetCore.Http;

namespace InventoryManager.Application.File;

public class Save : IFileSaver
{
    /// <summary>
    /// This functions saves the file to the database's hard drive.
    /// </summary>
    /// <returns>Returns URL to path of saved file</returns>
    public async Task<string> SaveFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
            //if Uploads folder doesn't exist, create it
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    
    }