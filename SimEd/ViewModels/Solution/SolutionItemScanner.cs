namespace SimEd.ViewModels.Solution;

internal class SolutionItemScanner
{
    public static SolutionItem ScanDirectory(DirectoryInfo dirInfo, GitIgnoreScanner scanner)
    {
        DirectoryInfo[] directoryInfos = dirInfo.GetDirectories();
        FileInfo[] fileInfos = dirInfo.GetFiles();
        SolutionItem result = new SolutionItem(dirInfo.Name, dirInfo.FullName, [], false);

        foreach (DirectoryInfo directory in directoryInfos)
        {
            if (scanner.IgnorePath(directory.FullName))
            {
                continue;
            }
            SolutionItem child = ScanDirectory(directory, scanner);
            result.Children.Add(child);
        }

        foreach (FileInfo file in fileInfos)
        {
            if (scanner.IgnorePath(file.FullName))
            {
                continue;
            }
            result.AddChild(file.Name, file.FullName);
        }

        return result;
    }
}