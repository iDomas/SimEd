using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using SimEd.Models.Languages;
using SimEd.Models.Languages.Common;
using SimEd.Models.Languages.CsharpLang;
using SimEd.ViewModels.Solution;

namespace SimEd.ViewModels.Search;

public class ShowGenericFinderWindowViewModel : ObservableObject
{
    private readonly SolutionLanguageExtractors _extractions;
    private string _typesText = string.Empty;

    public ShowGenericFinderWindowViewModel(SolutionViewModel solution, SolutionLanguageExtractors extractions)
    {
        _extractions = extractions;
        BuildIndexTask = BuildIndex(solution);
    }

    public Task<SolutionIndex>? BuildIndexTask { get; set; }

    public string TypesText
    {
        get => _typesText;
        set => SetProperty(ref _typesText, value);
    }

    public ObservableCollection<FindItemViewModel> FoundTypes { get; private set; } =
    [
        new FindItemViewModel()
        {
            FileName = "File1.cs"
        }
    ];

    private async Task<SolutionIndex> BuildIndex(SolutionViewModel solution)
    {
        var sw = Stopwatch.StartNew();
        SolutionItem[] files = solution.Nodes.Leafs(it => it.Children).ToArray();
        SolutionIndex result = new();
        foreach (SolutionItem solutionItem in files)
        {
            SolutionIndexItem[] resultedItems = await _extractions.Parse(solutionItem);
            result.Items.AddRange(resultedItems);
        }

        sw.Stop();

        Console.WriteLine(sw.Elapsed);

        return result;
    }
}