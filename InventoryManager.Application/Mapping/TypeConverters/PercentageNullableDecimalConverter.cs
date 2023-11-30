using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace InventoryManager.Application.Mapping.TypeConverters;

public class PercentageNullableDecimalConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        if (decimal.TryParse(text.TrimEnd('%'), out decimal result))
        {
            // Divide by 100 to convert percentage to a decimal value
            return result / 100;
        }

        // Return null if conversion fails
        return null;
    }
}