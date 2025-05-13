using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaEdit;
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
/*
        _textMateInstallation = Editor.InstallTextMate(_registryOptions);
       
        Language csharpLanguage = _registryOptions.GetLanguageByExtension(".cs");
        string scopeName = _registryOptions.GetScopeByLanguageId(csharpLanguage.Id);

        _textMateInstallation.SetGrammar(scopeName);
        */
    }

    private async void OnOpen(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        var files = await dialog.ShowAsync(this);
        if (files is null) return;
        foreach (var fileName in files)
        {
            var fileInfo = new FileInfo(fileName);
            TabItem addedTabItem = new TabItem();
            addedTabItem.Header = fileInfo.Name;
            TextEditor textEditor = new TextEditor();
            textEditor.Text = await File.ReadAllTextAsync(fileName);
            addedTabItem.Content = textEditor;
            
            //DocumentsTabArea.Items.Add(addedTabItem);
        }
    }
}