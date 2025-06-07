using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using SimEd.Models.Settings;

namespace SimEd.Models;

public class AppSettingsReader : IAppSettingsReader
{
    string SettingsJsonPath => Path.Combine(AppCustomDirectories.SettingsDirectory, "appsettings.json");

    public AppSettings Get()
    {
        if (!File.Exists(SettingsJsonPath))
        {
            return new AppSettings();
        }

        string jsonString = File.ReadAllText(SettingsJsonPath);
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return new AppSettings();
        }

        AppSettings? settings =
            JsonSerializer.Deserialize<AppSettings>(jsonString, CodeGen.SourceGenerationContext.Default.AppSettings);
        return settings ?? new AppSettings();
    }

    public void Write(AppSettings settings)
    {
        FileSystemHelper.CreateDirectory(AppCustomDirectories.SettingsDirectory);
        JsonTypeInfo<AppSettings> defaultAppSettings = CodeGen.SourceGenerationContext.Default.AppSettings;
        string jsonString = JsonSerializer.Serialize(settings, defaultAppSettings);
        File.WriteAllText(SettingsJsonPath, jsonString);
    }

    public void Update(Action<AppSettings> settingsChanged)
    {
        var appSettings = Get();
        settingsChanged.Invoke(appSettings);
        Write(appSettings);
    }
}