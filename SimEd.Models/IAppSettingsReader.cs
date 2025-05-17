namespace SimEd.Models;

public interface IAppSettingsReader
{
    Task<AppSettings> Read();
    Task Write(AppSettings settings);
}
