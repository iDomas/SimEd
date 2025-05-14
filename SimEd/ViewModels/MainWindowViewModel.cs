using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Controls;
using Dock.Model.Core;
using SimEd.Interfaces;
using SimEd.ViewModels.Documents;

namespace SimEd.ViewModels;

public class MainWindowViewModel : ObservableObject, IDropTarget
{
    private readonly IFactory? _factory;
    private IRootDock? _layout;

    public ServicesProvider Provider { get; }

    public IRootDock? Layout
    {
        get => _layout;
        set => SetProperty(ref _layout, value);
    }

    public MainWindowViewModel()
    {
        _factory = new NotepadFactory();

        Layout = _factory?.CreateLayout();
        if (Layout is { })
        {
            _factory?.InitLayout(Layout);
        }

        Provider = new ServicesProvider();
    }

    private Encoding GetEncoding(string path)
    {
        using var reader = new StreamReader(path, Encoding.Default, true);
        if (reader.Peek() >= 0)
        {
            reader.Read();
        }

        return reader.CurrentEncoding;
    }

    private async Task<FileViewModel> OpenFileViewModel(string path)
    {
        var encoding = GetEncoding(path);
        string text = await File.ReadAllTextAsync(path, encoding);
        string title = Path.GetFileName(path);
        return new FileViewModel()
        {
            Path = path,
            Title = title,
            Text = text,
            Encoding = encoding.WebName
        };
    }

    private void SaveFileViewModel(FileViewModel fileViewModel)
    {
        File.WriteAllText((string)fileViewModel.Path, (string?)fileViewModel.Text,
            Encoding.GetEncoding((string)fileViewModel.Encoding));
    }

    private void UpdateFileViewModel(FileViewModel fileViewModel, string path)
    {
        fileViewModel.Path = path;
        fileViewModel.Title = Path.GetFileName(path);
    }

    private void AddFileViewModel(FileViewModel fileViewModel)
    {
        var files = _factory?.GetDockable<IDocumentDock>("Files");
        if (Layout is { } && files is { })
        {
            _factory?.AddDockable(files, fileViewModel);
            _factory?.SetActiveDockable(fileViewModel);
            _factory?.SetFocusedDockable(Layout, fileViewModel);
        }
    }

    private FileViewModel? GetFileViewModel()
    {
        var files = _factory?.GetDockable<IDocumentDock>("Files");
        return files?.ActiveDockable as FileViewModel;
    }

    private FileViewModel GetUntitledFileViewModel()
    {
        return new FileViewModel
        {
            Path = string.Empty,
            Title = "Untitled",
            Text = "",
            Encoding = Encoding.Default.WebName
        };
    }

    public void CloseLayout()
    {
        if (Layout is IDock dock)
        {
            if (dock.Close.CanExecute(null))
            {
                dock.Close.Execute(null);
            }
        }
    }

    public void FileNew()
    {
        var untitledFileViewModel = GetUntitledFileViewModel();
        AddFileViewModel(untitledFileViewModel);
    }

    public async void FileOpen()
    {
        var storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
        {
            return;
        }

        var results = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()        {
            Title = "Open layout",
            FileTypeFilter = GetOpenOpenLayoutFileTypes(),
            AllowMultiple = false
        });

        if (results is { Count: > 0 })
        {
            foreach (var storageFile in results)
            {
                var path = storageFile.Path.AbsolutePath;
                if (!string.IsNullOrEmpty(path))
                {
                    FileViewModel untitledFileViewModel = await OpenFileViewModel(path);
                    AddFileViewModel(untitledFileViewModel);
                }
            }
        }
    }

    private static List<FilePickerFileType> GetOpenOpenLayoutFileTypes() =>
    [
        StorageService.CSharp,
        StorageService.Java,
        StorageService.Json,
        StorageService.All
    ];

    public async void FileSave()
    {
        if (GetFileViewModel() is { } fileViewModel)
        {
            if (string.IsNullOrEmpty(fileViewModel.Path))
            {
                await FileSaveAsImpl(fileViewModel);
            }
            else
            {
                SaveFileViewModel(fileViewModel);
            }
        }
    }

    public async void FileSaveAs()
    {
        if (GetFileViewModel() is { } fileViewModel)
        {
            await FileSaveAsImpl(fileViewModel);
        }
    }

    private async Task FileSaveAsImpl(FileViewModel fileViewModel)
    {
        var dlg = new SaveFileDialog
        {
            Filters = new List<FileDialogFilter>
            {
                new() { Name = "Text document", Extensions = { "txt" } },
                new() { Name = "All", Extensions = { "*" } }
            },
            InitialFileName = fileViewModel.Title,
            DefaultExtension = "txt"
        };
        var window = GetWindow();
        if (window is null)
        {
            return;
        }

        var result = await dlg.ShowAsync(window);
        if (result is { })
        {
            if (!string.IsNullOrEmpty(result))
            {
                UpdateFileViewModel(fileViewModel, result);
                SaveFileViewModel(fileViewModel);
            }
        }
    }

    public void FileExit()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            desktopLifetime.Shutdown();
        }
    }

    public void DragOver(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataFormats.Files))
        {
            e.DragEffects = DragDropEffects.None;
            e.Handled = true;
        }
    }

    public async void Drop(object? sender, DragEventArgs e)
    {
        if (e.Data.Contains(DataFormats.Files))
        {
            var result = e.Data.GetFileNames();
            if (result is { })
            {
                foreach (var path in result)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        var untitledFileViewModel = await OpenFileViewModel(path);
                        AddFileViewModel(untitledFileViewModel);
                    }
                }
            }

            e.Handled = true;
        }
    }

    private Window? GetWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            return desktopLifetime.MainWindow;
        }

        return null;
    }
}