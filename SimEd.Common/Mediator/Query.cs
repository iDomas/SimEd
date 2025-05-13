namespace SimEd.Common.Mediator;

public class Query<T, TR> : BaseQuery
{
    private Func<T, TR> _handler = _ => default!;

    public void Set(Func<T, TR> handler)
        => _handler = handler;

    public TR Call(T value)
        => _handler(value);
}
