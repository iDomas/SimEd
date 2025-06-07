using SimEd.Models.Settings;

namespace SimEd.Models;

public interface IAppSettingsReader
{
    AppSettings Get();
    void Write(AppSettings settings);
    void Update(Action<AppSettings> settingsChanged);
}
