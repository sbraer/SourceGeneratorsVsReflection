using SourceGeneratorsVsReflection.Models;

namespace SourceGeneratorsVsReflection.Utilities;

public static class Files
{
    public static string GetCsvPath(string subDir)
    {
        Console.WriteLine(Environment.CurrentDirectory);
        var path = new Uri(Environment.CurrentDirectory);
        var newPath = path.Segments[0];
        for (var counter = 1; counter < path.Segments.Length; counter++)
        {
            var pathSegment = path.Segments[counter];
            newPath = Path.Combine(newPath, pathSegment);
            if (pathSegment.Equals("SourceGeneratorsVsReflection" + Path.AltDirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            {
                newPath = Path.Combine(newPath, subDir);
                break;
            }
        }

        if (!Directory.Exists(newPath))
        {
            Directory.CreateDirectory(newPath);
        }

        return newPath;
    }

    public static void CreateCsvFile(List<RandomPropertiesClass> randomData, string csvFilePath)
    {
        using StreamWriter writer = new(csvFilePath);
        // Header
        writer.WriteLine(string.Join(",", typeof(RandomPropertiesClass).GetProperties().Select(p => p.Name)));

        // Write the data
        foreach (var item in randomData)
        {
            var values = typeof(RandomPropertiesClass).GetProperties()
                .Select(p =>
                {
                    var value = p.GetValue(item)?.ToString() ?? "";
                    // manage comma and double quote
                    value = value.Replace(",", string.Empty);
                    return value.Contains(',') ? $"\"{value.Replace("\"", "\"\"")}\"" : value;
                });
            writer.WriteLine(string.Join(",", values));
        }
    }
}
