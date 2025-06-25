namespace SimEd.Models.Languages.Lexer;

public record struct Token(ArraySegment<char> Text, int Position, string Kind)
{
    override public string ToString()
        => $"{new string(Text.ToArray())}: {Kind}";
}