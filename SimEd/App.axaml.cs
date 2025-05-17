using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using SimEd.IoC;
using SimEd.Models;
using SimEd.ViewModels;
using SimEd.Views;

namespace SimEd;

public partial class App : Application
{
    private ServiceProvider _serviceProvider;

    public override void Initialize()
    {
        _serviceProvider = ServicesTools.Initialize();
        var viewLocator = _serviceProvider.GetService<IDataTemplate>();
        this.DataTemplates.Add(viewLocator);
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // DockManager.s_enableSplitToWindow = true;

        var appSettings = new AppSettingsReader();
        var defaultSettings = new AppSettings();
        appSettings.Write(defaultSettings);

        MainWindowViewModel mainWindowViewModel = _serviceProvider.GetService<MainWindowViewModel>();

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktopLifetime:
            {
                var mainWindow = new MainWindow
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
                var mainView = new MainView()
                {
                    DataContext = mainWindowViewModel
                };

                singleViewLifetime.MainView = mainView;

                break;
            }
        }

        base.OnFrameworkInitializationCompleted();
#if DEBUG
        this.AttachDevTools();
#endif
    }
}