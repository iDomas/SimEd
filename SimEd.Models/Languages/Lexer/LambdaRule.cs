namespace SimEd.Models.Languages.Lexer;

public class LambdaRule : BaseRule
{
    private readonly Func<ArraySegment<char>, int> _func;

    public LambdaRule(string kind, Func<ArraySegment<char>, int> func)
        : base(kind)
        => _func = func;

    public override int Match(ArraySegment<char> segment)
        => _func(segment);
}