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
    private string _solutionPath = Directory.GetCurrentDirectory();

    public SolutionViewModel(IMiniPubSub pubSub)
    {
        _pubSub = pubSub;
        _pubSub.AddCommand<ChangeSolutionFolderCommand>(OnChangeSolutionFolder);
    }

    private void OnChangeSolutionFolder(ChangeSolutionFolderCommand changeSolutionFolder)
    {
        SolutionPath = changeSolutionFolder.FolderName;
        var dirInfo = new DirectoryInfo(SolutionPath);
        if (!dirInfo.Exists)
        {
            return;
        }

        var root = SolutionItemScanner.ScanDirectory(dirInfo);
        Nodes.Clear();
        Nodes.AddRange(root.Children);
    }


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
            if (SetProperty(ref _solutionPath, value))
            {
                _pubSub.Command(new ChangeSolutionFolderCommand(value));
            }
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
        _pubSub.Command(new ChangeSolutionFolderCommand(SolutionPath));
    }
}