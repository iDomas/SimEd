using SimEd.Models.Languages.Common;
using ZLinq;

namespace SimEd.Models.Languages.CsharpLang;

public class SolutionLanguageExtractors
{
    public IDeclarationsExtraction[] Extractions { get; set; } = BuildDefaultList();

    private static IDeclarationsExtraction[] BuildDefaultList()
    {
        List<IDeclarationsExtraction> extractions =
        [
            new CsDeclarationsExtraction()
        ];
        return extractions.ToArray();
    }

    public async Task<SolutionIndexItem[]> Parse(SolutionItem solutionItem)
    {
        IDeclarationsExtraction? extraction = Extractions
            .AsValueEnumerable()
            .FirstOrDefault(it => it.IsFileMatcher(solutionItem.Path));
        if (extraction == null)
        {
            return [];
        }

        string dataBytes = await File.ReadAllTextAsync(solutionItem.Path)
            .ConfigureAwait(false);
        SolutionIndexItem[] items = extraction.ExtractFileDefinitions(
            solutionItem.Path,
            dataBytes.ToCharArray());
        return items;
    }
}