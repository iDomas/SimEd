namespace SimEd.Common.Mediator;

public class TypedDictionaryOfValues<TValue>
{
    private readonly Dictionary<string, int> _mapTaskId = [];
    public List<TValue> Values { get; } = [];

    public int IndexOf<T>()
    {
        var key = typeof(T).Name;
        if (_mapTaskId.TryGetValue(key, out var index))
        {
            return index;
        }

        return -1;
    }

    public TValue UpdatedValue<TAdded>(Func<TValue> create)
    {
        var index = IndexOf<TAdded>();
        if (index != -1)
        {
            return Values[index];
        }

        TValue addedValue = create();
        var actualIndex = Values.Count;
        _mapTaskId.Add(typeof(TAdded).Name, actualIndex);
        Values.Add(addedValue);
        return addedValue;
    }
}
