using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Utils;
using Dock.Model.Mvvm.Controls;
using SimEd.Common.Interfaces;
using SimEd.Interfaces;
using SimEd.Views.Solution;

namespace SimEd.ViewModels.Solution;

public class SolutionViewModel : Tool, IViewAware
{
    private readonly IMiniPubSub _pubSub;

    public ObservableCollection<SolutionItem> Nodes { get; set; } = [];
    public SolutionView View { get; set; }

    public SolutionItem? Selected { get; set; }

    public SolutionViewModel(IMiniPubSub pubSub)
    {
        _pubSub = pubSub;
    }

    private string _solutionPath = Directory.GetCurrentDirectory();

    public void SelectedInSolutionDoubleTapped(object sender, TappedEventArgs e)
    {
        if (Selected != null)
        {
            _pubSub.Publish(new FileIsOpened(Selected));
        }
    }

    public string SolutionPath
    {
        get => _solutionPath;
        set
        {
            SetProperty(ref _solutionPath, value);
            UpdateView();
        }
    }

    public async void OnSolutionChosen()
    {
        var storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
        {
            return;
        }

        var results = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Open Solution",
            AllowMultiple = false
        });

        if (results is { Count: > 0 })
        {
            var selectedDir = results[0].Path;
            SolutionPath = new DirectoryInfo(selectedDir.AbsolutePath).FullName;
        }
    }

    public void SetControl(Control control)
    {
        View = (SolutionView)control;
        UpdateView();
    }

    private void UpdateView()
    {
        var dirInfo = new DirectoryInfo(SolutionPath);
        if (!dirInfo.Exists)
        {
            return;
        }

        var root = SolutionItemScanner.ScanDirectory(dirInfo);
        Nodes.Clear();
        Nodes.AddRange(root.Children);
    }
}