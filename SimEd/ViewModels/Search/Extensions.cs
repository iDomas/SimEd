namespace SimEd.ViewModels.Search;

internal static class Extensions
{
    public static IEnumerable<T> Leafs<T>(this IList<T> items, Func<T, IList<T>> childSelector)
    {
        foreach (T item in items)
        {
            IList<T> leaf = childSelector(item);
            if (leaf.Count == 0)
            {
                yield return item;
                break;
            }

            IEnumerable<T> leafsIterator = Leafs<T>(leaf, childSelector);
            foreach (T resultedLeaf in leafsIterator)
            {
                yield return resultedLeaf;
            }
        }
    }
    
}