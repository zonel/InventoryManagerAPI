using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace InventoryManager.Application.Mapping.TypeConverters;

public class NullableIntConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            // Return null for empty or whitespace strings
            return null;
        }

        if (int.TryParse(text, out int result))
        {
            // If conversion to int succeeds, return the value
            return result;
        }
        
        // Return null if conversion fails
        return null;
    }
}