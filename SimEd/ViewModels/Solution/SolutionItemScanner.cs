namespace SimEd.ViewModels.Solution;

internal class SolutionItemScanner
{
    public static SolutionItem ScanDirectory(DirectoryInfo dirInfo)
    {
        var result = new SolutionItem(dirInfo.Name, dirInfo.FullName, []);
        foreach (var directory in dirInfo.GetDirectories())
        {
            var child = ScanDirectory(directory);
            result.Children.Add(child);
        }

        foreach (var file in dirInfo.GetFiles())
        {
            result.AddChild(file.Name, file.FullName);
        }

        return result;
    }
}