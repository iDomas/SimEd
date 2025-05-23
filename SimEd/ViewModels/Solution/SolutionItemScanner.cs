namespace SimEd.ViewModels.Solution;

internal class SolutionItemScanner
{
    public static SolutionItem ScanDirectory(DirectoryInfo dirInfo)
    {
        DirectoryInfo[] directoryInfos = dirInfo.GetDirectories();
        FileInfo[] fileInfos = dirInfo.GetFiles();
        int countChildren = directoryInfos.Length + fileInfos.Length;
        SolutionItem result = new SolutionItem(dirInfo.Name, dirInfo.FullName, [], countChildren != 0);

        foreach (DirectoryInfo directory in directoryInfos)
        {
            SolutionItem child = ScanDirectory(directory);
            result.Children.Add(child);
        }

        foreach (FileInfo file in fileInfos)
        {
            result.AddChild(file.Name, file.FullName);
        }

        return result;
    }
}