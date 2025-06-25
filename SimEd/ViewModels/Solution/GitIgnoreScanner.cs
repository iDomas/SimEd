using ZLinq;

namespace SimEd.ViewModels.Solution;

public class GitIgnoreScanner
{
    public Func<string, bool>[] IgnoredFiles { get; set; } = [];

    public void ScanDirectory(DirectoryInfo directoryInfo)
    {
        Clear();

        string gitIgnoreFile = Path.Combine(directoryInfo.FullName, ".gitignore");
        if (!File.Exists(gitIgnoreFile))
        {
            return;
        }

        string[] lines = File.ReadAllLines(gitIgnoreFile);
        string[] goodFiles = lines
            .AsValueEnumerable()
            .Select(l => l.Trim())
            .Where(x => x.Length > 0 && x[0] != '#')
            .ToArray();
        IgnoredFiles = BuildFilters(goodFiles);
    }

    private static Func<string, bool>[] BuildFilters(string[] goodFiles)
    {
        List<Func<string, bool>> filters =
        [
            x =>
            {
                string gitFolder = $"{Path.DirectorySeparatorChar}.git";
                return x.EndsWith(gitFolder);
            },
            x =>
            {
                string gitFolder = $"{Path.DirectorySeparatorChar}.gitignore";
                return x.EndsWith(gitFolder);
            }
        ];
        
        foreach (string file in goodFiles)
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