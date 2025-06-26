using SimEd.Models.Languages.Lexer;

namespace SimEd.Models.Languages.CsharpLang;

internal static class DefaultCsScanner
{
    public static SimpleScanner CsScanner { get; } = BuildScanner();

    private static char[][] BuildOperatorsArray()
    {
        string[] operators =
        [
            ".", ",", ";", ":",
            "+=", "-=", "*=", "/=",
            "+", "-", "*", "/",
            "==",
            "!", "?",
            ">=", "<=", "<", ">",
            "=>",
            "$",
            "&&", "&", "||", "|",
            "(", ")",
            "[", "]",
            "{", "}",
            "=",
        ];
        return operators
            .Select(c => c.ToCharArray())
            .ToArray();
    }

    private static char[][] BuildReservedWordsArray()
    {
        string[] operators =
        [
            "class", "record", "interface", "struct", "enum", "delegate",
            "public", "protected", "internal", "private",
            "namespace", "using",
            "return", "abstract", "as", "base", "break", "case", "catch",
        ];
        return operators
            .Select(c => c.ToCharArray())
            .ToArray();
    }

    private static SimpleScanner BuildScanner() 
        =>new()
        {
            Rules = GetCsLexRulesList()
        };

    private static BaseRule[] GetCsLexRulesList()
    {
        List<BaseRule> list =
        [
            new LambdaRule(TokenKindsCSharp.Spaces, SpacesMatch),
            new LambdaRule(TokenKindsCSharp.Eoln, EolnMatch),
            new LambdaRule(TokenKindsCSharp.Comment, CommentMatch),
            new LambdaRule(TokenKindsCSharp.QuotedString, StringMatch),
            new LambdaRule(TokenKindsCSharp.Operator, OperatorsMatch),
            new LambdaRule(TokenKindsCSharp.Reserved, ReservedMatch),
            new LambdaRule(TokenKindsCSharp.Identifier, IdentifierMatch),
            new LambdaRule(TokenKindsCSharp.Number, NumberMatch),
        ];

        return list.ToArray();
    }

    private static int CommentMatch(ArraySegment<char> text)
    {
        if (text.Count < 2)
        {
            return 0;
        }

        if (text[0] != '/' || text[1] != '/')
        {
            return 0;
        }

        for (var i = 2; i < text.Count; i++)
        {
            if (text[i] == '\n' || text[i] == '\r')
            {
                return i + 1;
            }
        }

        return text.Count;
    }

    private static int StringMatch(ArraySegment<char> arg)
    {
        if (arg[0] != '"' && arg[0] != '\'')
        {
            return 0;
        }

        for (var i = 1; i < arg.Count; i++)
        {
            if (arg[i] == arg[0])
            {
                return i + 1;
            }
        }

        return arg.Count;
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
        => segment.MatchInSegmentByLambda(c => c == ' ' || c == '\t');

    private static int EolnMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(c => c == '\n' || c == '\r');


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


    private static int NumberMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(IsMatchForNumber);


    private static int IdentifierMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(IsMatchStartForIdentifier, IsMatchForIdentifier);

    static bool IsMatchStartForIdentifier(char c)
        => Char.IsLetter(c) || c == '_';

    static bool IsMatchForNumber(char c)
        => Char.IsDigit(c);

    static bool IsMatchForIdentifier(char c)
        => IsMatchStartForIdentifier(c) || Char.IsDigit(c);
}