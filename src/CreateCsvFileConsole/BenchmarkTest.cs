using BenchmarkDotNet.Attributes;
using SourceGeneratorsVsReflection.Models;
using SourceGeneratorsVsReflection.ReflectionSimpleTest;
using SourceGeneratorsVsReflection.ReflectionSpan;
using SourceGeneratorsVsReflection.SourceGeneratorsTest;

namespace SourceGeneratorsVsReflection;

//[ShortRunJob]
[MemoryDiagnoser]
[RankColumn]
public class BenchmarkTest
{
    private readonly string[] allRowsFile = File.ReadAllLines(Configuration.GetCsvFileNameWithPath);

    [Benchmark]
    public List<RandomPropertiesClass> GetEasyReflection() => MapWithEasyReflection.Import(allRowsFile);

    [Benchmark]
    public List<RandomPropertiesClass> GetReflectionSpan() => MapWithSpanReflection.Import(allRowsFile);

    [Benchmark]
    public List<RandomPropertiesClass> GetSourceGenerator() => MapWithSourceGenerators.Import(allRowsFile);
}
