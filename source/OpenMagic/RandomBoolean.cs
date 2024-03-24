using System.Collections.Generic;

namespace OpenMagic;

public static class RandomBoolean
{
    /// <summary>
    ///     Get an enumerable of random booleans.
    /// </summary>
    /// <param name="count">The number of random booleans to return.</param>
    public static IEnumerable<bool> Enumerable(int count)
    {
        return RandomNumber.Enumerable(count, Next);
    }

    /// <summary>
    ///     Returns a random <see cref="bool" />.
    /// </summary>
    public static bool Next()
    {
        return RandomNumber.NextInt(0, 2) == 1;
    }
}