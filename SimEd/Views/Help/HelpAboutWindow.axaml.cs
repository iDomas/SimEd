using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

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