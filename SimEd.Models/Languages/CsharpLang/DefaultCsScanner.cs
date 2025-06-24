using SimEd.Models.Languages.Lexer;

namespace SimEd.Models.Languages.CsharpLang;

internal static class DefaultCsScanner
{
    public static SimpleScanner CsScanner { get; } = BuildScanner();

    private static char[][] BuildOperatorsArray()
    {
        string[] operators =
        [
            ".", ",", ";",
            "==", "=",
            "(", ")",
            "{", "}",
        ];
        return operators
            .Select(c => c.ToCharArray())
            .ToArray();
    }

    private static char[][] BuildReservedWordsArray()
    {
        string[] operators =
        [
            "class", "record", "interface", "struct", "enum", "delegate", "enum",
            "public", "protected", "internal", "private",
            "return", "abstract", "as", "base", "break", "case", "catch",
        ];
        return operators
            .Select(c => c.ToCharArray())
            .ToArray();
    }
    private static SimpleScanner BuildScanner()
    {
        SimpleScanner result = new();
        result.Rules = GetCsLexRulesList();
        return result;
    }

    private static BaseRule[] GetCsLexRulesList()
    {
        List<BaseRule> list =
        [
            new LambdaRule(TokenKindsCSharp.Spaces, SpacesMatch),
            new LambdaRule(TokenKindsCSharp.Operator, OperatorsMatch),
            new LambdaRule(TokenKindsCSharp.Reserved, ReservedMatch),
            new LambdaRule(TokenKindsCSharp.Identifier, IdentifierMatch),
        ];

        return list.ToArray();
    }

    private static readonly char[][] Operators = BuildOperatorsArray();

    private static int OperatorsMatch(ArraySegment<char> arg)
    {
        var operators = Operators;
        return MatchArrayOfWordsLength(arg, operators);
    }

    private static int MatchArrayOfWordsLength(ArraySegment<char> arg, char[][] wordsToMatch)
    {
        var firstChar = arg[0];
        foreach (var op in wordsToMatch)
        {
            if (op[0] != firstChar)
            {
                continue;
            }

            var found = true;
            for (var i = 1; i < op.Length; i++)
            {
                if (op[i] != arg[i])
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                return op.Length;
            }
        }

        return 0;
    }

    private static int SpacesMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(c => Char.IsWhiteSpace(c));


    private static readonly char[][] ReservedWords = BuildReservedWordsArray();


    private static int ReservedMatch(ArraySegment<char> segment)
    {
        var matchIdentifier = IdentifierMatch(segment);
        if (matchIdentifier == 0)
        {
            return 0;
        }

        var matchReservedLength = MatchArrayOfWordsLength(segment, ReservedWords);
        if (matchReservedLength == 0 || matchIdentifier != matchReservedLength)
        {
            return 0;
        }

        return matchReservedLength;
    }

    private static int IdentifierMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(IsMatchStartForIdentifier, IsMatchForIdentifier);

    static bool IsMatchStartForIdentifier(char c)
        => Char.IsLetter(c) || c == '_';

    static bool IsMatchForIdentifier(char c)
        => IsMatchStartForIdentifier(c) || Char.IsDigit(c);
}