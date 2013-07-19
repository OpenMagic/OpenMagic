using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenMagic.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Gets a random item from the list.
        /// </summary>
        /// <remarks>
        /// If <paramref name="list"/> is null then null is returned.
        /// </remarks>
        public static T RandomItem<T>(this IEnumerable<T> list)
        {
            // todo: unit tests

            if (list == null)
            {
                return default(T);
            }

            var randomIndex = RandomNumber.NextInt(0, list.Count());

            return list.ElementAt(randomIndex);
        }

        /// <summary>
        /// Gets a random item from the list that does not equal <paramref name="doesNotEqual"/>
        /// </summary>
        /// <remarks>
        /// If <paramref name="list"/> is null then null is returned.
        /// </remarks>
        public static T RandomItem<T>(this IEnumerable<T> list, T doesNotEqual)
        {
            // todo: unit tests

            list.MustNotBeNull("list");

            T thisItem = RandomItem(list);

            while (thisItem.Equals(doesNotEqual))
            {
                thisItem = RandomItem(list);
            }

            return thisItem;
        }
    }
}
