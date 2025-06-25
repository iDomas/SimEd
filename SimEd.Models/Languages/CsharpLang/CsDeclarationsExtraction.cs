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

        var tokens = scanner.Tokenize(fileData, SkipSpaces).ToArray();
        return BuildDeclarationsFromTokens(tokens, fileName);
    }

    private SolutionIndexItem[] BuildDeclarationsFromTokens(Token[] tokens, string fileName)
    {
        var resultList = new List<SolutionIndexItem>();
        for (var index = 0; index < tokens.Length; index++)
        {
            var token = tokens[index];
            if (!IsDeclaration(token))
            {
                continue;
            }

            resultList.Add(new SolutionIndexItem(tokens[index + 1].GetText(), fileName, token.GetText()));
        }

        return resultList.ToArray();
    }

    public static bool IsDeclaration(Token token)
    {
        if (token.Kind != TokenKindsCSharp.Reserved)
        {
            return false;
        }

        string tokenText = token.GetText();
        return tokenText switch
        {
            "class" or "struct" or "record" or "interface" or "enum" => true,
            _ => false
        };
    }

    private bool SkipSpaces(Token token)
    {
        return token.Kind switch
        {
            TokenKindsCSharp.Spaces => false,
            _ => true
        };
    }
}