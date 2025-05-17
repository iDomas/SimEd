namespace SimEd.Models;

public static class AppCustomDirectories
{
    public static string LocalAppData => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public static string SettingsDirectory => Path.Combine(LocalAppData, "SimEd");
}