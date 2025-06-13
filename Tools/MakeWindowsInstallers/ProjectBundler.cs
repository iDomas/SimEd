using System.Diagnostics;

namespace MakeWindowsInstallers;

internal static class ProjectBundler
{
    public static async Task Bundle(string project, string platformName)
    {
        string arguments = $"publish {project} -r {platformName} -c Release";
        Process p = Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = arguments,
            RedirectStandardOutput = true
        })!;
        await p.WaitForExitAsync().ConfigureAwait(false);
        string output = await p.StandardOutput.ReadToEndAsync();
        string[] lines = output.Split(Environment.NewLine);
        string publishedLine = lines.Last(line => line.Contains("publish"));
        string path = publishedLine.Split(" -> ")[1].Trim();

        string[] allowedExtensions = [".dll", ".exe"];
        string[] filesToZip = Directory
            .EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
            .Where(f => allowedExtensions.Contains(Path.GetExtension(f)))
            .ToArray();
        
        string outputZipFile = $"{project}_{platformName}.zip";
        File.Delete(outputZipFile);
        ZipFileCreator.CreateZipFile(outputZipFile, filesToZip);
    }
}