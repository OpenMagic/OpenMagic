using System;

namespace OpenMagic
{
    /// <summary>
    ///     Collection of methods to get a random number.
    /// </summary>
    public static class RandomNumber
    {
        private static readonly Random Random = new Random();

        /// <summary>
        ///     Returns a random <see cref="int" />.
        /// </summary>
        public static int NextInt()
        {
            return NextInt(int.MinValue, int.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="int" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static int NextInt(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }
    }
}