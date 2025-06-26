﻿using SimEd.Models.Languages.CsharpLang;

namespace SimEd.Models.Languages.CurlyBasedLanguages;

public static class CurlyLexerRules
{
    public static char[][] BuildCharsArrays(string[] operators)
    {
        return operators
            .Select(c => c.ToCharArray())
            .ToArray();
    }

    public static int CommentMatch(ArraySegment<char> text)
    {
        if (text.Count < 2 || text[0] != '/')
        {
            return 0;
        }

        switch (text[1])
        {
            case '/':
            {
                for (var i = 2; i < text.Count; i++)
                {
                    if (text[i] == '\n' || text[i] == '\r')
                    {
                        return i + 1;
                    }
                }

                return text.Count;
            }
            case '*':
            {
                for (var i = 2; i < text.Count - 1; i++)
                {
                    if (text[i] == '*' && text[i + 1] == '/')
                    {
                        return i + 2;
                    }
                }


                return text.Count;
            }
        }

        return 0;
    }

    public static int StringMatch(ArraySegment<char> arg)
    {
        if (arg[0] != '"' && arg[0] != '\'')
        {
            return 0;
        }

        var i = 1;
        while (i < arg.Count)
        {
            if (arg[i] == '\\')
            {
                i += 2;
                continue;
            }

            if (arg[i] == arg[0])
            {
                return i + 1;
            }

            i++;
        }

        return arg.Count;
    }


    public static int SpacesMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(c => c == ' ' || c == '\t');

    public static int EolnMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(c => c == '\n' || c == '\r');

    public static int IdentifierMatch(ArraySegment<char> segment)
        => segment.MatchInSegmentByLambda(IsMatchStartForIdentifier, IsMatchForIdentifier);

    static bool IsMatchStartForIdentifier(char c)
        => Char.IsLetter(c) || c == '_';

    static bool IsMatchForIdentifier(char c)
        => IsMatchStartForIdentifier(c) || Char.IsDigit(c);

    public static int MatchArrayOfWordsLength(ArraySegment<char> arg, char[][] wordsToMatch)
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
}