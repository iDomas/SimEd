using Avalonia.Controls;
using AvaloniaEdit.TextMate;
using Dock.Model.Mvvm.Controls;
using SimEd.Interfaces;
using SimEd.Views.Documents;
using TextMateSharp.Grammars;

namespace SimEd.ViewModels.Documents;

public class FileViewModel : Document, IViewAware
{
    public FileViewModel()
    {
        
    }
    public string Path
    {
        get => _path;
        set => SetProperty(ref _path, value);
    }

    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public string Encoding
    {
        get => _encoding;
        set => SetProperty(ref _encoding, value);
    }
    public FileView MainControl { get; set; }
    
    private string _path = string.Empty;
    private string _text = string.Empty;
    private string _encoding = string.Empty;

    public void SetControl(Control control)
    {
        MainControl = (FileView)control;

        UpdateView();
    }

    private void UpdateView()
    {
        var registryOptions = new RegistryOptions(ThemeName.Light);
        var textMateInstallation = MainControl.MainTextEditor.InstallTextMate(registryOptions);
        var extension = ExtensionOfFile(this.Path);
        if (string.IsNullOrEmpty(extension))
        {
            extension = ".txt";
        }
        Language csharpLanguage = registryOptions.GetLanguageByExtension(extension);
        string scopeName = registryOptions.GetScopeByLanguageId(csharpLanguage?.Id ?? "");
        textMateInstallation.SetGrammar(scopeName);
    }

    private static string ExtensionOfFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return string.Empty;
        }
        FileInfo fileInfo = new FileInfo(fileName);
        if (!fileInfo.Exists)
        {
            return string.Empty;
        }

        return fileInfo.Extension;
    }

}