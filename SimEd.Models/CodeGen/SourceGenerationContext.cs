using System.Text.Json.Serialization;
using SimEd.Models.Settings;

namespace SimEd.Models.CodeGen;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(AppSettings))]
public partial class SourceGenerationContext : JsonSerializerContext;
