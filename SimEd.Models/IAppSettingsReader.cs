namespace SimEd.Models;

public interface IAppSettingsReader
{
    AppSettings Read();
    void Write(AppSettings settings);
    void Update(Action<AppSettings> settingsChanged);
}
