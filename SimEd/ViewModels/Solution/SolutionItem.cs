using System.Collections.ObjectModel;

namespace SimEd.ViewModels.Solution;

public record SolutionItem(string Name, string Path, ObservableCollection<SolutionItem> Children, bool IsExpanded)
{
    public SolutionItem AddChild(string name, string path)
    {
        SolutionItem child = new SolutionItem(name, path, [], true);
        Children.Add(child);
        return child;
    }
}