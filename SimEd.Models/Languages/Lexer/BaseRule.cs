namespace SimEd.Models.Languages.Lexer;

public abstract class BaseRule(string kind)
{
    public string Kind { get; } = kind;
    public abstract int Match(ArraySegment<char> segment);
}