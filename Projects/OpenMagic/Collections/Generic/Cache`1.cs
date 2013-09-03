using System;
using System.Collections.Generic;

namespace OpenMagic.Collections.Generic
{
    /// <summary>
    /// Represents a cache for <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the cache.</typeparam>
    /// <typeparam name="TValue">The type of the values in the cache.</typeparam>
    /// <remarks>
    /// <see cref="Cache<TKey, TValue>"/> extends <see cref="Dictionary<TKey, TValue>"/> with the <see cref="Cache<TKey, TValue>.Get(Func<TValue> valueFactory)"/> method.
    /// </remarks>
    public class Cache<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets the value for <typeparamref name="TKey"/> from the cache. If <typeparamref name="TKey"/> is not in cache
        /// then the result of <paramref name="valueFactory"/> is added to the cache and returned.
        /// </summary>
        /// <param name="valueFactory">The function to invoke if <typeparamref name="TKey"/> is not in the cache.</param>
        public TValue Get(TKey key, Func<TValue> valueFactory)
        {
            TValue value;

            if (this.TryGetValue(key, out value))
            {
                return value;
            }

            value = valueFactory();
            this.Add(key, value);

            return value;
        }
    }
}
