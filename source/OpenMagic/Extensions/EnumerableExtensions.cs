using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace OpenMagic.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    ///     Gets a random item from the list.
    /// </summary>
    /// <remarks>
    ///     If <paramref name="list" /> is null then null is returned.
    /// </remarks>
    public static T RandomItem<T>([AllowNull] this IEnumerable<T> list)
    {
        if (list == null)
        {
            return default;
        }

        var items = list.ToArray();
        var randomIndex = RandomNumber.NextInt(0, items.Length);

        return items[randomIndex];
    }

    /// <summary>
    ///     Gets a random item from the list that does not equal <paramref name="doesNotEqual" />
    /// </summary>
    /// <remarks>
    ///     If <paramref name="list" /> is null then null is returned.
    /// </remarks>
    public static T RandomItem<T>(this IEnumerable<T> list, T doesNotEqual)
    {
        var items = list.ToArray();
        var thisItem = RandomItem(items);

        while (thisItem.Equals(doesNotEqual))
        {
            thisItem = RandomItem(items);
        }

        return thisItem;
    }
}