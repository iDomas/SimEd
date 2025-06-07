using ZLinq;

namespace SimEd.ViewModels.Solution;

public class GitIgnoreScanner
{
    public Func<string, bool>[] IgnoredFiles { get; set; } = [];

    public void ScanDirectory(DirectoryInfo directoryInfo)
    {
        Clear();

        var gitIgnoreFile = Path.Combine(directoryInfo.FullName, ".gitignore");
        if (!File.Exists(gitIgnoreFile))
        {
            return;
        }

        var lines = File.ReadAllLines(gitIgnoreFile);
        var goodFiles = lines
            .AsValueEnumerable()
            .Select(l => l.Trim())
            .Where(x => x.Length > 0 && x[0] != '#')
            .ToArray();
        this.IgnoredFiles = BuildFilters(goodFiles);
    }

    private static Func<string, bool>[] BuildFilters(string[] goodFiles)
    {
        List<Func<string, bool>> filters = [];
        foreach (var file in goodFiles)
        {
            filters.Add((fullFileName) => fullFileName.Contains(file));
        }

        return filters.ToArray();
    }

    private void Clear()
    {
        IgnoredFiles = [];
    }

    public bool IgnorePath(string directoryFullName)
        => IgnoredFiles
            .AsValueEnumerable()
            .Any(ignoredFileFilter => ignoredFileFilter(directoryFullName));
}