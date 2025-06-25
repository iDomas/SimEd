using System.Collections.ObjectModel;

namespace SimEd.Models.Languages;

public record SolutionItem(string Name, string Path, ObservableCollection<SolutionItem> Children, bool IsExpanded)
{
    public SolutionItem AddChild(string name, string path)
    {
        SolutionItem child = new SolutionItem(name, path, [], true);
        Children.Add(child);
        return child;
    }
}