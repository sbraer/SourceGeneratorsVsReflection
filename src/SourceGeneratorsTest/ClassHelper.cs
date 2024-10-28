using GeneratorsFromAttributeExample;
using SourceGeneratorsVsReflection.Models;

namespace SourceGeneratorsVsReflection.SourceGeneratorsTest;

[GenerateSetProperty<RandomPropertiesClass>()]
internal static partial class ClassHelper { }