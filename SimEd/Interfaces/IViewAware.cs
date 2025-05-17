using Avalonia.Controls;

namespace SimEd.Interfaces;

public interface IViewAware
{
    void SetControl (Control control);
}