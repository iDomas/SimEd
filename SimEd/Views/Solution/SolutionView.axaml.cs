using Avalonia.Controls;
using Avalonia.Input;
using SimEd.ViewModels.Solution;

namespace SimEd.Views.Solution;

public partial class SolutionView : UserControl
{
    public SolutionViewModel ViewModel => (SolutionViewModel)DataContext!;
    public SolutionView()
    {
        InitializeComponent();
    }

    private void SelectedInSolutionDoubleTapped(object? sender, TappedEventArgs e)
    {
        ViewModel.SelectedInSolutionDoubleTapped(sender, e);
    }
}
