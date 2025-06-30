﻿using SimEd.Models.Languages.CsharpLang;
using SimEd.Models.Languages.CurlyBasedLanguages;
using SimEd.Models.Languages.Lexer;

namespace SimEd.Models.Languages.JavaLang;

internal static class JavaScanner
{
    public static SimpleScanner Instance { get; } = BuildScanner();

    private static char[][] BuildOperatorsArray()
        => CurlyLexerRules.BuildCharsArrays([
            ".", ",", ";", ":", "%", "^",
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
            
            "@",
        ]);


    private static char[][] BuildReservedWordsArray()
        => CurlyLexerRules.BuildCharsArrays([
            "class", "record", "interface", "enum",
            "public", "protected", "private",
            "package",
            "return", "abstract", "as", "base", "break", "case", "catch",
        ]);

    private static SimpleScanner BuildScanner()
        => new()
        {
            Rules =
            [
                new LambdaRule(TokenKindsJava.Spaces, CurlyLexerRules.SpacesMatch),
                new LambdaRule(TokenKindsJava.Eoln, CurlyLexerRules.EolnMatch),
                new LambdaRule(TokenKindsJava.Comment, CurlyLexerRules.CommentMatch),
                new LambdaRule(TokenKindsJava.QuotedString, CurlyLexerRules.StringMatch),
                new LambdaRule(TokenKindsJava.Operator, OperatorsMatch),
                new LambdaRule(TokenKindsJava.Reserved, ReservedMatch),
                new LambdaRule(TokenKindsJava.Identifier, CurlyLexerRules.IdentifierMatch),
                new LambdaRule(TokenKindsJava.Number, NumberMatch),
            ]
        };

    private static readonly char[][] Operators = BuildOperatorsArray();

    private static int OperatorsMatch(ArraySegment<char> arg)
        => CurlyLexerRules.MatchArrayOfWordsLength(arg, Operators);


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