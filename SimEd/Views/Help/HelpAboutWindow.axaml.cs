using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SimEd.Views.Help;

public partial class HelpAboutWindow : Window
{
    public HelpAboutWindow()
    {
        InitializeComponent();
    }

    private void OnClicked(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}