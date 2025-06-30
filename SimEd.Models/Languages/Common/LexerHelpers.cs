namespace SimEd.Models.Languages.CsharpLang;

public static class LexerHelpers
{
    public static int MatchInSegmentByLambda(this ArraySegment<char> segment, Func<char, bool> predicate)
    {
        var pos = 0;
        foreach (char b in segment)
        {
            if (!predicate(b))
            {
                return pos;
            }

            pos++;
        }

        return segment.Count;
    }

    public static int MatchInSegmentByLambda(this ArraySegment<char> segment, Func<char, bool> predicateFirst,
        Func<char, bool> predicate)
    {
        if (!predicateFirst(segment[0]))
        {
            return 0;
        }

        segment = segment[1..];
        var pos = 1;
        foreach (char b in segment)
        {
            if (!predicate(b))
            {
                return pos;
            }

            pos++;
        }

        return segment.Count;
    }
}