using System.Collections.ObjectModel;

namespace SimEd.ViewModels.Solution;

public readonly record struct SolutionItem(string Name, string Path, ObservableCollection<SolutionItem> Children)
{
    public SolutionItem AddChild(string name, string path)
    {
        var child = new SolutionItem(name, path, []);
        Children.Add(child);
        return child;
    }

    public void ClearChildren()
    {
        Children.Clear();
    }
}