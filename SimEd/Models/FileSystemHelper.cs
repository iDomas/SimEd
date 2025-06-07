namespace SimEd.Models;

internal static class FileSystemHelper
{
    public static void CreateDirectory(this string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}