using Avalonia.Controls;
using AvaloniaEdit.TextMate;
using Dock.Model.Mvvm.Controls;
using SimEd.Views.Documents;
using TextMateSharp.Grammars;

namespace SimEd.ViewModels.Documents;

public class FileViewModel : Document, IViewAware
{
    private string _path = string.Empty;
    private string _text = string.Empty;
    private string _encoding = string.Empty;

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

    public Control Control
    {
        set
        {
            MainControl = (FileView)value;

            UpdateView();
        }
    }

    private void UpdateView()
    {
        var registryOptions = new RegistryOptions(ThemeName.Light);
        var textMateInstallation = MainControl.MainTextEditor.InstallTextMate(registryOptions);
        Language csharpLanguage = registryOptions.GetLanguageByExtension(".cs");
        string scopeName = registryOptions.GetScopeByLanguageId(csharpLanguage.Id);
        textMateInstallation.SetGrammar(scopeName);
        
    }

    public FileView MainControl { get; set; }
}
