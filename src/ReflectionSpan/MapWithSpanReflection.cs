using SourceGeneratorsVsReflection.Models;
using System.Reflection;

namespace SourceGeneratorsVsReflection.ReflectionSpan;

public static class MapWithSpanReflection
{
    public static List<RandomPropertiesClass> Import(string[] allRowsFile)
    {
        List<RandomPropertiesClass> loadedData = [];

        try
        {
            // read header
            string[] headers = allRowsFile[0].Split(',');
            List<PropertyInfo> propertiesInfo = [];
            foreach (var header in headers)
            {
                propertiesInfo.Add(typeof(RandomPropertiesClass).GetProperty(header)!);
            }

            for (var counter = 1; counter < allRowsFile.Length; counter++)
            {
                // read the data
                var reader = allRowsFile[counter].AsSpan();

                RandomPropertiesClass item = new();

                var index = reader.IndexOf(',');
                for (var h = 0; h < headers.Length; h++)
                {
                    PropertyInfo prop = propertiesInfo[h];
                    object value;
                    
                    if (h != headers.Length - 1)
                    {
                        value = Convert.ChangeType(reader.Slice(0, index).ToString(), prop.PropertyType);
                        reader = reader.Slice(index + 1);
                        index = reader.IndexOf(',');
                    }
                    else
                    {
                        value = Convert.ChangeType(reader.ToString(), prop.PropertyType);
                    }
                    
                    prop.SetValue(item, value);
                }

                loadedData.Add(item);
            }

            return loadedData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Generic error: {ex.Message}");
            return [];
        }
    }
}
