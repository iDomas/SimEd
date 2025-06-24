using SimEd.Models.Languages.Common;
using SimEd.Models.Languages.Lexer;

namespace SimEd.Models.Languages.CsharpLang;

public class CsDeclarationsExtraction : IDeclarationsExtraction
{
    public bool IsFileMatcher(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        return extension == ".cs";
    }

    public SolutionIndexItem[] ExtractFileDefinitions(string fileName, char[] fileData)
    {
        SimpleScanner scanner = DefaultCsScanner.CsScanner;

        var data = scanner.Tokenize(fileData).ToArray();

        return [];
    }
}