namespace Availability;

public static class CollectionExtensions
{
    public static int CalculateHashCode<T>(this ISet<T> collection) where T : class
    {
        var sum = 0;
        foreach (var element in collection)
        {
            sum = unchecked(sum + element.GetHashCode());
        }

        return sum;
    }
    
    public static int CalculateHashCode<T>(this IList<T> collection) where T : class
    {
        return collection
            .Aggregate(0, (hash, pair) => HashCode.Combine(hash, pair.GetHashCode()));
    }

    public static int CalculateHashCode<TKey, TValue>(this IDictionary<TKey, TValue> collection)
    {
        return CalculateHashCode(collection, value => value == null ? 0 : value.GetHashCode());
    }
    
    // Based on: https://docs.oracle.com/en/java/javase/21/docs/api/java.base/java/util/Map.html#hashCode()
    public static int CalculateHashCode<TKey, TValue>(this IDictionary<TKey, TValue> collection, Func<TValue, int> getHashCode)
    {
        var sum = 0;
        foreach (var element in collection)
        {
            sum = unchecked(sum + (element.Value == null ? 0 : getHashCode(element.Value)));
        }

        return sum;
    }

    
    public static bool DictionaryEqual<TKey, TValue>(this IDictionary<TKey, TValue> first,
        IDictionary<TKey, TValue> second, IEqualityComparer<TValue>? valueComparer = null)
    {
        if (ReferenceEquals(first, second)) return true;
        if (first.Count != second.Count) return false;

        valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;

        foreach (var kvp in first)
        {
            if (!second.TryGetValue(kvp.Key, out var secondValue)) return false;
            if (!valueComparer.Equals(kvp.Value, secondValue)) return false;
        }

        return true;
    }
}