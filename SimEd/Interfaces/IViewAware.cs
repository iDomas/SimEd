using Avalonia.Controls;

namespace SimEd.ViewModels;

public interface IViewAware
{
    void SetControl (Control control);
}