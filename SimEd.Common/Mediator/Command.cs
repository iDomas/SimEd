namespace SimEd.Common.Mediator;

public class Command<T> : BaseCommand
{
    private Action<T> Run { get; set; } = _ => { };

    public void Set(Action<T> handler)
        => Run = handler;

    public void Call(T value)
        => Run(value);
}
