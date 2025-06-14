namespace MakeWindowsInstallers;

internal static class OsDetector
{
    internal static OsKind DetectOsKind
        => Environment.OSVersion.Platform switch
        {
            PlatformID.Unix => OsKind.Linux,
            PlatformID.MacOSX => OsKind.MacOs,
            _ => OsKind.Windows
        };

    internal static string GetParentOfSolution()
    {
        string directory = Directory.GetCurrentDirectory();
        DirectoryInfo? directoryInfo = new DirectoryInfo(directory);
        while (directoryInfo.Parent != null)
        {
            string gitIgnoreCombineFileName = Path.Combine(directoryInfo.FullName, ".gitignore");
            if (File.Exists(gitIgnoreCombineFileName))
            {
                return directoryInfo.FullName;
            }

            directoryInfo = directoryInfo.Parent;
        }

        return string.Empty;
    }
}