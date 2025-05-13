namespace SimEd.Common.Mediator;

public interface IMiniPubSub
{
    void Publish<T>(T value);
    MiniPubSub AddEvent<T>(Action<T> handler);
    IMiniPubSub AddCommand<T>(Action<T> handler);
    IMiniPubSub AddQuery<T, TR>(Func<T, TR> handler);


    HandlerList<T> GetHandlesOfType<T>();
    Command<T> GetCommandOfType<T>();
    Query<T, TR> GetQueryOfType<T, TR>();
}
