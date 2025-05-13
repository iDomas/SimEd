using Avalonia.Controls;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace SimEd;

public partial class MainWindow : Window
{
    private readonly RegistryOptions _registryOptions;
    private readonly TextMate.Installation _textMateInstallation;

    public MainWindow()
    {
        InitializeComponent();
        
        _registryOptions = new RegistryOptions(ThemeName.DarkPlus);

        _textMateInstallation = Editor.InstallTextMate(_registryOptions);
       
        Language csharpLanguage = _registryOptions.GetLanguageByExtension(".cs");
        string scopeName = _registryOptions.GetScopeByLanguageId(csharpLanguage.Id);

        _textMateInstallation.SetGrammar(scopeName);
    }
}