namespace SimEd.Models;

internal static class FileSystemExtensions
{
    public static void CreateDirectory(this string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}