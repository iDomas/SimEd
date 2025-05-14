using System.Collections.Generic;
using System.IO;
using Avalonia.Platform.Storage;
using Dock.Model.Mvvm.Controls;

namespace SimEd.ViewModels.Solution;

public class SolutionViewModel : Tool
{
    private string _solutionPath = Directory.GetCurrentDirectory();

    public string SolutionPath
    {
        get => _solutionPath;
        set => SetProperty(ref _solutionPath, value);
    }

    public async void OnSolutionChosen()
    {
        var storageProvider = StorageService.GetStorageProvider();
        if (storageProvider is null)
        {
            return;
        }

        var results = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()        {
            Title = "Open Solution",
            AllowMultiple = false
        });

        if (results is { Count: > 0 })
        {
            var selectedDir = results[0].Path;
            SolutionPath = new DirectoryInfo(selectedDir.AbsolutePath).FullName;
        }
        
    }

    private static List<FilePickerFileType> GetOpenOpenLayoutFileTypes() =>
    [
        StorageService.CSharp,
        StorageService.Java,
        StorageService.Json,
        StorageService.All
    ];
}