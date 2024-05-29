using System;
using System.Collections.Generic;

namespace OpenMagic
{
    public static class RandomDateTime
    {
        /// <summary>
        ///     Get an enumerable of random <see cref="DateTime" /> values.
        /// </summary>
        /// <param name="count">
        ///     The number of random <see cref="DateTime" /> values to return.
        /// </param>
        public static IEnumerable<DateTime> Enumerable(int count)
        {
            return RandomNumber.Enumerable(count, Next);
        }

        /// <summary>
        ///     Get an enumerable of random <see cref="DateTime" /> values.
        /// </summary>
        /// <param name="count">
        ///     The number of random <see cref="DateTime" /> values to return.
        /// </param>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        public static IEnumerable<DateTime> Enumerable(int count, DateTime minValue, DateTime maxValue)
        {
            return RandomNumber.Enumerable(count, () => Next(minValue, maxValue));
        }

        /// <summary>
        ///     Returns a random <see cref="DateTime" />.
        /// </summary>
        public static DateTime Next()
        {
            return Next(DateTime.MinValue, DateTime.MaxValue);
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
        ///     A DateTime greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static DateTime Next(DateTime minValue, DateTime maxValue)
        {
            var millisecondsDifference = maxValue.Subtract(minValue).TotalMilliseconds;
            var randomMilliseconds = RandomNumber.NextDouble(0, millisecondsDifference);
            var value = minValue.AddMilliseconds(randomMilliseconds);

            return value;
        }
    }
}