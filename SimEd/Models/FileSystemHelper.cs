using System.IO;

namespace SimEd.Models;

static class FileSystemHelper
{
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}