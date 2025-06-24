using SimEd.Models.Languages.Common;

namespace SimEd.Models.Languages.Languages;

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
            .FirstOrDefault(it => it.IsFileMatcher(solutionItem.Path));
        if (extraction == null)
        {
            return [];
        }

        return [];
    }
}