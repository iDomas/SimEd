using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm.Controls;
using SimEd.ViewModels.Documents;

namespace SimEd.ViewModels.Tools;

public class FindViewModel : Tool
{
    private string _find = string.Empty;

    public string Find
    {
        get => _find;
        set => SetProperty(ref _find, value);
    }

    public void FindNext()
    {
        if (Context is not IRootDock root || root.ActiveDockable is not IDock active)
        {
            return;
        }

        if (active.Factory?.FindDockable(active, (d) => d.Id == "Files") is not IDock files)
        {
            return;
        }
        if (files.ActiveDockable is FileViewModel fileViewModel)
        {
            // TODO: 
        }
    }
}
