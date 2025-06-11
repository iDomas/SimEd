using System.Diagnostics;
using System.IO.Compression;

class ProjectBundler
{
    public async Task Bundle(string project, string platformName)
    {
        string arguments = $"publish {project} -r {platformName} -c Release";
        var p = Process.Start(new ProcessStartInfo()
        {
            FileName = "dotnet",
            Arguments = arguments,
            RedirectStandardOutput = true
        });
        await p.WaitForExitAsync();
        var output = await p.StandardOutput.ReadToEndAsync();
        var lines = output.Split(Environment.NewLine);
        var publishedLine = lines.LastOrDefault(line => line.Contains("publish"));
        var path = publishedLine.Split(" -> ")[1].Trim();

        var allowedExtensions = new[] { ".dll", ".exe" };
        var filesToZip = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
            .Where(f => allowedExtensions.Contains(Path.GetExtension(f)));
        
        var outputZipFile = $"{project}_{platformName}.zip";
        File.Delete(outputZipFile);
        ZipFileCreator.CreateZipFile(outputZipFile, filesToZip);
    }
}