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
        set
        {
            SetProperty(ref _typesText, value);
            UpdateFilter();
        }
    }

    private void UpdateFilter()
    {
        var values = this.BuildIndexTask.Result.Items.Where(it => it.Name.Contains(TypesText)).ToArray();;
        FoundTypes.Clear();
        foreach (var value in values)
        {
            FoundTypes.Add(new FindItemViewModel()
            {
                FileName = value.Name
            });
        }
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
        List<Task<SolutionIndexItem[]>> tasks = [];
        tasks.AddRange(files.Select(solutionItem => _extractions.Parse(solutionItem)));
        //await Task.WhenAll(tasks).ConfigureAwait(false);
        foreach (var task in tasks)
        {
            await task.ConfigureAwait(false);
        }

        foreach (Task<SolutionIndexItem[]> solutionItem in tasks)
        {
            SolutionIndexItem[] resultedItems = solutionItem.Result;
            result.Items.AddRange(resultedItems);
        }

        sw.Stop();

        Console.WriteLine(sw.Elapsed);

        return result;
    }
}