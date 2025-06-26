using SimEd.Models.Languages.CurlyBasedLanguages;
using SimEd.Models.Languages.Lexer;

namespace SimEd.Models.Languages.CsharpLang;

internal static class CsScanner
{
    public static SimpleScanner Instance { get; } = BuildScanner();

    private static char[][] BuildOperatorsArray()
        => CurlyLexerRules.BuildCharsArrays([
            ".", ",", ";", ":", "%","^",
            "~",
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
        ]);

    private static char[][] BuildReservedWordsArray()
        => CurlyLexerRules.BuildCharsArrays([
            "class", "record", "interface", "struct", "enum", "delegate",
            "public", "protected", "internal", "private",
            "namespace", "using",
            "return", "abstract", "as", "base", "break", "case", "catch",
        ]);

    private static SimpleScanner BuildScanner()
        => new()
        {
            Rules =
            [
                new LambdaRule(TokenKindsCSharp.Spaces, SpacesMatch),
                new LambdaRule(TokenKindsCSharp.Eoln, EolnMatch),
                new LambdaRule(TokenKindsCSharp.Comment, CurlyLexerRules.CommentMatch),
                new LambdaRule(TokenKindsCSharp.QuotedString, CurlyLexerRules.StringMatch),
                new LambdaRule(TokenKindsCSharp.Operator, OperatorsMatch),
                new LambdaRule(TokenKindsCSharp.Reserved, ReservedMatch),
                new LambdaRule(TokenKindsCSharp.Identifier, CurlyLexerRules.IdentifierMatch),
                new LambdaRule(TokenKindsCSharp.Number, NumberMatch),
            ]
        };

    private static readonly char[][] Operators = BuildOperatorsArray();

    private static int OperatorsMatch(ArraySegment<char> arg)
    {
        var operators = Operators;
        return CurlyLexerRules.MatchArrayOfWordsLength(arg, operators);
    }

    private static int SpacesMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(c => c == ' ' || c == '\t');

    private static int EolnMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(c => c == '\n' || c == '\r');


    private static readonly char[][] ReservedWords = BuildReservedWordsArray();


    private static int ReservedMatch(ArraySegment<char> segment)
    {
        var matchIdentifier = CurlyLexerRules.IdentifierMatch(segment);
        if (matchIdentifier == 0)
        {
            return 0;
        }

        var matchReservedLength = CurlyLexerRules.MatchArrayOfWordsLength(segment, ReservedWords);
        if (matchReservedLength == 0 || matchIdentifier != matchReservedLength)
        {
            return 0;
        }

        return matchReservedLength;
    }


    private static int NumberMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(IsMatchForNumber);


    static bool IsMatchForNumber(char c)
        => Char.IsDigit(c);
}