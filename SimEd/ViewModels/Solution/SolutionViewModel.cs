using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Utils;
using Dock.Model.Mvvm.Controls;
using SimEd.Common.Interfaces;
using SimEd.Events;
using SimEd.Interfaces;
using SimEd.ViewModels.Documents;
using SimEd.Views.Solution;

namespace SimEd.ViewModels.Solution;

public class SolutionViewModel : Tool, IViewAware
{
    private readonly IMiniPubSub _pubSub;

    public ObservableCollection<SolutionItem> Nodes { get; set; } = [];
    public SolutionView View { get; set; }

    public SolutionItem? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    private string _solutionPath = Directory.GetCurrentDirectory();
    private SolutionItem? _selected;

    public SolutionViewModel(IMiniPubSub pubSub)
    {
        _pubSub = pubSub;
        _pubSub.AddCommandHandler<ChangeSolutionFolderCommand>(OnChangeSolutionFolder);
        _pubSub.AddEventHandler<ChangedFocusedTab>(OnChangedFocusedTab);
    }

    private void OnChangedFocusedTab(ChangedFocusedTab focused)
    {
        string selectedPath = Selected?.Path ?? "";
        if (selectedPath == focused.FileViewModel.Path)
        {
            return;
        }

        SelectFocusedFile(Nodes, focused.FileViewModel);
    }

    private bool SelectFocusedFile(Collection<SolutionItem> nodes, FileViewModel focusedFileViewModel)
    {
        foreach (SolutionItem solutionItem in nodes)
        {
            if (SelectFocusedFile(solutionItem.Children, focusedFileViewModel))
            {
                return true;
            }

            if (solutionItem.Path == focusedFileViewModel.Path)
            {
                this.Selected = solutionItem;
                return true;
            }
        }

        return false;
    }

    private void OnChangeSolutionFolder(ChangeSolutionFolderCommand changeSolutionFolder)
    {
        SolutionPath = changeSolutionFolder.FolderName;
        DirectoryInfo dirInfo = new DirectoryInfo(SolutionPath);
        if (!dirInfo.Exists)
        {
            return;
        }

        SolutionItem root = SolutionItemScanner.ScanDirectory(dirInfo);
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
        IStorageProvider? storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
        {
            return;
        }

        IReadOnlyList<IStorageFolder> results = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Open Solution",
            AllowMultiple = false
        });

        if (results is { Count: > 0 })
        {
            Uri selectedDir = results[0].Path;
            SolutionPath = new DirectoryInfo(selectedDir.AbsolutePath).FullName;
        }
    }

    public void SetControl(Control control)
    {
        View = (SolutionView)control;
        _pubSub.Command(new ChangeSolutionFolderCommand(SolutionPath));
    }
}