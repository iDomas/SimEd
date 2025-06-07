using SimEd.Common.Mediator;

namespace SimEd.Common.Interfaces;

public interface IMiniPubSub
{
    void Publish<T>(T value);
    TR Query<T, TR>(T value);
    void Command<T>(T value);
    IMiniPubSub AddEventHandler<T>(Action<T> handler);
    IMiniPubSub AddCommandHandler<T>(Action<T> handler);
    IMiniPubSub AddQueryHandler<T, TR>(Func<T, TR> handler);


    HandlerList<T> GetHandlesOfType<T>();
    Command<T> GetCommandOfType<T>();
    Query<T, TR> GetQueryOfType<T, TR>();
    void RemoveEventHandler<T>(Action<T> onZoomChanged);
}