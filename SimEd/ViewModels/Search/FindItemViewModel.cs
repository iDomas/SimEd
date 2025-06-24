using CommunityToolkit.Mvvm.ComponentModel;

namespace SimEd.ViewModels.Search;

public class FindItemViewModel : ObservableObject
{
    public string FileName { get; set; } = string.Empty;
    public string ClassName {get; set; }  = string.Empty;
    public int LineNumber {get; set;}
    public int ColumnNumber {get; set;}
}