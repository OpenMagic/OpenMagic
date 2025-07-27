using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenMagic.Extensions.Collections.Generic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Invokes
        ///     <param name="action" />
        ///     on each item in
        ///     <param name="collection" />
        ///     .
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">
        ///     The action to invoke on each item in
        ///     <param name="collection" />
        ///     .
        /// </param>
        public static void ForEach<TItem>(this IEnumerable<TItem> collection, Action<TItem> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        ///     Indicates whether a specified enumerable is null or empty.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? value) => value == null || !value.Any();
    }
}