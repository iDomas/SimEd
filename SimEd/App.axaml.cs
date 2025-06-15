using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using SimEd.Common.Interfaces;
using SimEd.IoC.Tools;
using SimEd.ViewModels;
using SimEd.Views;

namespace SimEd;

public partial class App : Application
{
    private IInjector _injector;

    public override void Initialize()
    {
        _injector = ServicesTools.Initialize();
        DataTemplates.Add(_injector.GetService<IDataTemplate>());
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        MainWindowViewModel mainWindowViewModel = _injector.GetService<MainWindowViewModel>();

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktopLifetime:
            {
                MainWindow mainWindow = new ()
                {
                    DataContext = mainWindowViewModel
                };

                mainWindow.Closing += (_, _) => { mainWindowViewModel.CloseLayout(); };

                desktopLifetime.MainWindow = mainWindow;

                desktopLifetime.Exit += (_, _) => { mainWindowViewModel.CloseLayout(); };

                break;
            }
            case ISingleViewApplicationLifetime singleViewLifetime:
            {
                MainView mainView = new ()
                {
                    DataContext = mainWindowViewModel
                };

                singleViewLifetime.MainView = mainView;

                break;
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}