using System;
using System.Collections.Generic;

namespace OpenMagic.Collections.Generic;

/// <summary>
///     Represents a cache for <typeparamref name="TKey" />.
/// </summary>
/// <typeparam name="TKey">The type of the keys in the cache.</typeparam>
/// <typeparam name="TValue">The type of the values in the cache.</typeparam>
public class Cache<TKey, TValue> : Dictionary<TKey, TValue>
{
    /// <summary>
    ///     Gets the value for <typeparamref name="TKey" /> from the cache. If <typeparamref name="TKey" /> is not in cache
    ///     then the result of <paramref name="valueFactory" /> is added to the cache and returned.
    /// </summary>
    /// <param name="key">The key of the element to get.</param>
    /// <param name="valueFactory">The function to invoke if <typeparamref name="TKey" /> is not in the cache.</param>
    public TValue Get(TKey key, Func<TValue> valueFactory)
    {
        if (TryGetValue(key, out var value))
        {
            return value;
        }

        value = valueFactory();
        Add(key, value);

        return value;
    }
}