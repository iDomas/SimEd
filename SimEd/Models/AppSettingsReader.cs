using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimEd.Models;

public class AppSettingsReader
{
    string SettingsJsonPath => Path.Combine(AppCustomDirectories.SettingsDirectory, "appsettings.json");

    public AppSettings Read()
    {
        if (!File.Exists(SettingsJsonPath))
        {
            return new AppSettings();
        }
        var jsonString = File.ReadAllText(SettingsJsonPath);
        AppSettings appSettings = JsonSerializer.Deserialize<AppSettings>(jsonString, SourceGenerationContext.Default.AppSettings)!;
        return appSettings;
    }

    public async Task Write(AppSettings settings)
    {
        FileSystemHelper.CreateDirectory(AppCustomDirectories.SettingsDirectory);
        var jsonString = JsonSerializer.Serialize(
            settings, SourceGenerationContext.Default.AppSettings);

        await File.WriteAllTextAsync(SettingsJsonPath, jsonString);
    }
}