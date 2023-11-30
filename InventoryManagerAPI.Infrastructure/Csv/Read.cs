using InventoryManagerAPI.Domain.Csv;
using Serilog;

namespace InventoryManagerAPI.Infrastructure.Csv;

public class Read : ICsvFileReader
{
    public async Task<IEnumerable<string>> ReadFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File '{filePath}' not found.");
        }

        var lines = new List<string>();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Error reading file: {ex.Message}");
            throw;
        }

        return lines;
    } 
}