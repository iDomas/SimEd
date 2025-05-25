using System.Text.Json;

namespace SimEd.Models;

public class AppSettingsReader : IAppSettingsReader
{
    string SettingsJsonPath => Path.Combine(AppCustomDirectories.SettingsDirectory, "appsettings.json");

    public AppSettings Read()
    {
        if (!File.Exists(SettingsJsonPath))
        {
            return new AppSettings();
        }

        string jsonString = File.ReadAllText(SettingsJsonPath);
        AppSettings appSettings =
            JsonSerializer.Deserialize<AppSettings>(jsonString, CodeGen.SourceGenerationContext.Default.AppSettings)?? new AppSettings();
        return appSettings;
    }

    public void Write(AppSettings settings)
    {
        FileSystemHelper.CreateDirectory(AppCustomDirectories.SettingsDirectory);
        string jsonString = JsonSerializer.Serialize(
            settings, CodeGen.SourceGenerationContext.Default.AppSettings);

        File.WriteAllText(SettingsJsonPath, jsonString);
    }

    public void Update(Action<AppSettings> settingsChanged)
    {
        var appSettings = Read();
        settingsChanged.Invoke(appSettings);
        Write(appSettings);
    }
}