using SimEd.Common.Mediator;

namespace SimEd.Common;

public static class MiniPubSubExtensions
{
    public static void Publish<T>(this IMiniPubSub pubSub) where T : new()
        => pubSub.Publish(new T());
}
