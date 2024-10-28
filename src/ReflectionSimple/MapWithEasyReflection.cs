using SourceGeneratorsVsReflection.Models;
using System.Reflection;

namespace SourceGeneratorsVsReflection.ReflectionSimpleTest;

public static class MapWithEasyReflection
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
                var reader = allRowsFile[counter];

                RandomPropertiesClass item = new();

                var cols = reader.Split(',');
                for (var h = 0; h < headers.Length; h++)
                {
                    PropertyInfo prop = propertiesInfo[h];
                    object value = Convert.ChangeType(cols[h], prop.PropertyType);
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
