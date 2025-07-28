using System;

namespace OpenMagic.Collections.Generic
{
    /// <summary>
    ///     Represents a cache where the key is <see cref="Type" />.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    public class TypeCache<TValue> : Cache<Type, TValue>
    {
        /// <summary>
        ///     Gets the value for <typeparamref name="TType" /> from the cache. If <typeparamref name="TType" /> is not in cache
        ///     then the result of <paramref name="valueFactory" /> is added to the cache and returned.
        /// </summary>
        /// <param name="valueFactory">The function to invoke if <typeparamref name="TType" /> is not in the cache.</param>
        public TValue Get<TType>(Func<TValue> valueFactory)
        {
            return Get(typeof(TType), valueFactory);
        }
    }
}