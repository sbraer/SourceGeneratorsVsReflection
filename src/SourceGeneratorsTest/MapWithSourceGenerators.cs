using SourceGeneratorsVsReflection.Models;

namespace SourceGeneratorsVsReflection.SourceGeneratorsTest;

public class MapWithSourceGenerators
{
    public static List<RandomPropertiesClass> Import(string[] allRowsFile)
    {
        List<RandomPropertiesClass> loadedData = [];

        try
        {
            for (var counter = 1; counter < allRowsFile.Length; counter++)
            {
                // read the data
                var reader = allRowsFile[counter];

                RandomPropertiesClass item = new();
                ClassHelper.SetPropertiesRandomPropertiesClass(item, reader);
                loadedData.Add(item);
            }

            return loadedData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Generic error: {ex.Message}");
#if(DEBUG)
            Console.WriteLine($"Error: {ex}");
#endif
            return [];
        }
    }
}
