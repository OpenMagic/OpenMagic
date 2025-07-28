using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenMagic
{
    /// <summary>
    ///     Collection of methods to get a random value.
    /// </summary>
    public static class RandomNumber
    {
        private static readonly Random Random = new();

        /// <summary>
        ///     Get an enumerable of random values.
        /// </summary>
        /// <typeparam name="T">Type of random values</typeparam>
        /// <param name="count">The number of random values.</param>
        /// <param name="randomValueFactory">The function to generate random values.</param>
        public static IEnumerable<T> Enumerable<T>(int count, Func<T> randomValueFactory)
        {
            return System.Linq.Enumerable.Range(0, count).Select(_ => randomValueFactory());
        }

        /// <summary>
        ///     Returns a random <see cref="byte" />.
        /// </summary>
        public static byte NextByte()
        {
            return NextByte(byte.MinValue, byte.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="byte" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A byte greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static byte NextByte(byte minValue, byte maxValue)
        {
            return (byte)NextInt(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="char" />.
        /// </summary>
        public static char NextChar()
        {
            return NextChar(char.MinValue, char.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="char" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A char greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static char NextChar(char minValue, char maxValue)
        {
            return (char)NextInt(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="decimal" />.
        /// </summary>
        public static decimal NextDecimal()
        {
            return NextDecimal(decimal.MinValue, decimal.MaxValue);
        }

        /// <summary>
        ///     Returns a random nullable <see cref="decimal" />.
        /// </summary>
        public static decimal? NextNullableDecimal()
        {
            return RandomBoolean.Next() ? NextDecimal() : null;
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
        ///     A decimal greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static decimal NextDecimal(decimal minValue, decimal maxValue)
        {
            return (decimal)NextDouble((double)minValue, (double)maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="double" />.
        /// </summary>
        public static double NextDouble()
        {
            return NextDouble(double.MinValue, double.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="double" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A double greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static double NextDouble(double minValue, double maxValue)
        {
            var randomValueBetween0And1 = Random.NextDouble();
            var range = maxValue - minValue;

            if (double.IsPositiveInfinity(range))
            {
                range = double.MaxValue;
            }

            var randomValue = randomValueBetween0And1 * range;

            return minValue + randomValue;
        }

        /// <summary>
        ///     Returns a random <see cref="float" />.
        /// </summary>
        public static float NextFloat()
        {
            return NextFloat(float.MinValue, float.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="float" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A float greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static float NextFloat(float minValue, float maxValue)
        {
            return (float)NextDouble(minValue, maxValue);
        }

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

        /// <summary>
        ///     Returns a random <see cref="long" />.
        /// </summary>
        public static long NextLong()
        {
            return NextLong(long.MinValue, long.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="long" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A long greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static long NextLong(long minValue, long maxValue)
        {
            return (long)NextDouble(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="sbyte" />.
        /// </summary>
        public static sbyte NextSByte()
        {
            return NextSByte(sbyte.MinValue, sbyte.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="sbyte" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A sbyte greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static sbyte NextSByte(sbyte minValue, sbyte maxValue)
        {
            return (sbyte)NextInt(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="short" />.
        /// </summary>
        public static short NextShort()
        {
            return NextShort(short.MinValue, short.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="short" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A short greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static short NextShort(short minValue, short maxValue)
        {
            return (short)NextInt(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="uint" />.
        /// </summary>
        public static uint NextUInt()
        {
            return NextUInt(uint.MinValue, uint.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="uint" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A uint greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static uint NextUInt(uint minValue, uint maxValue)
        {
            return (uint)NextDouble(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="ulong" />.
        /// </summary>
        public static ulong NextULong()
        {
            return NextULong(ulong.MinValue, ulong.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="ulong" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A ulong greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static ulong NextULong(ulong minValue, ulong maxValue)
        {
            return (ulong)NextDouble(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="ushort" />.
        /// </summary>
        public static ushort NextUShort()
        {
            return NextUShort(ushort.MinValue, ushort.MaxValue);
        }

        /// <summary>
        ///     Returns a random <see cref="ushort" /> within a specified range.
        /// </summary>
        /// <param name="minValue">
        ///     The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>
        ///     A ushort greater than or equal to minValue and less than maxValue; that is, the range of return values
        ///     includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.
        /// </returns>
        public static ushort NextUShort(ushort minValue, ushort maxValue)
        {
            return (ushort)NextUInt(minValue, maxValue);
        }
    }
}