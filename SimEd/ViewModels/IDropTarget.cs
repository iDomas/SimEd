using Avalonia.Input;

namespace SimEd.ViewModels;

public interface IDropTarget
{
    void DragOver(object? sender, DragEventArgs e);
    void Drop(object? sender, DragEventArgs e);
}