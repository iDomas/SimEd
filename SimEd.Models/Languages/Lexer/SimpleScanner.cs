namespace SimEd.Models.Languages.Lexer;

public class SimpleScanner
{
    public BaseRule[] Rules { get; set; } = [];

    public Token[] Tokenize(ArraySegment<char> segment)
    {
        List<Token> tokens = [];
        var pos = 0;

        while (segment.Count > 0)
        {
            bool found = false;
            foreach (var rule in Rules)
            {
                int matchLen = rule.Match(segment);
                if (matchLen == 0)
                {
                    continue;
                }

                Token token = new Token(segment.Slice(0, matchLen), pos, rule.Kind);
                tokens.Add(token);
                found = true;
                segment = segment.Slice(matchLen);
                pos += matchLen;
                break;
            }

            if (!found)
            {
                tokens.Add(new Token(segment, pos, "Unparsed Token"));
                return tokens.ToArray();
            }
        }

        return tokens.ToArray();
    }
}