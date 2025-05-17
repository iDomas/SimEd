using System.Text.Json.Serialization;

namespace SimEd.Models.CodeGen;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(AppSettings))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}