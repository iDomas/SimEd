using System.Text.Json;

namespace SimEd.Models;

public class AppSettingsReader : IAppSettingsReader
{
    string SettingsJsonPath => Path.Combine(AppCustomDirectories.SettingsDirectory, "appsettings.json");

    public async Task<AppSettings> Read()
    {
        if (!File.Exists(SettingsJsonPath))
        {
            return new AppSettings();
        }

        string jsonString = await File.ReadAllTextAsync(SettingsJsonPath).ConfigureAwait(false);
        AppSettings appSettings =
            JsonSerializer.Deserialize<AppSettings>(jsonString, CodeGen.SourceGenerationContext.Default.AppSettings)!;
        return appSettings;
    }

    public async Task Write(AppSettings settings)
    {
        FileSystemHelper.CreateDirectory(AppCustomDirectories.SettingsDirectory);
        var jsonString = JsonSerializer.Serialize(
            settings, CodeGen.SourceGenerationContext.Default.AppSettings);

        await File.WriteAllTextAsync(SettingsJsonPath, jsonString);
    }
}