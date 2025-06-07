using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm.Controls;
using SimEd.ViewModels.Documents;

namespace SimEd.ViewModels.Tools;

public class ReplaceViewModel : Tool
{
    private string _find = string.Empty;
    private string _replace = string.Empty;

    public string Find
    {
        get => _find;
        set => SetProperty(ref _find, value);
    }

    public string Replace
    {
        get => _replace;
        set => SetProperty(ref _replace, value);
    }

    public void ReplaceNext()
    {
        if (Context is not IRootDock root || root.ActiveDockable is not IDock active) return;
        if (active.Factory?.FindDockable(active, (d) => d.Id == "Files") is IDock files)
        {
            if (files.ActiveDockable is FileViewModel fileViewModel)
            {
                // TODO: 
            }
        }
    }
}
