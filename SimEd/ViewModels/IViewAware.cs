using Avalonia.Controls;

namespace SimEd.ViewModels;

public interface IViewAware
{
    Control Control { set; }
}