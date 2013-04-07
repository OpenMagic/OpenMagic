using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Anotar.NLog;
using NullGuard;

namespace OpenMagic.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get the values in a string that are between a pair of delimiters.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <param name="delimiter">The value delimiter.</param>
        /// <example>
        /// GetValuesBetween("a 'quick' brown 'fox'") => { "quick", "fox }.
        /// </example>
        public static IEnumerable<string> GetValuesBetween(this string value, [AllowNull] string delimiter)
        {
            Log.Trace("GetValuesBetween(value: {0}, delimiter: {1})", value, delimiter);
            
            if (string.IsNullOrWhiteSpace(delimiter))
            {
                throw new ArgumentException("Value cannot be whitespace.", "delimiter");
            }

            var values = value.Split(Convert.ToChar(delimiter));

            foreach (string delimitedValue in values)
            {
                yield return delimitedValue;
            }
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        /// Syntactic sugar.
        /// </remarks>
        public static bool IsNullOrWhiteSpace([AllowNull] this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Splits a string value into lines.
        /// </summary>
        /// <param name="value">The string to split into lines.</param>
        public static IEnumerable<string> ToLines([AllowNull] this string value)
        {
            return value.ToLines(false);
        }

        /// <summary>
        /// Splits a string value into lines.
        /// </summary>
        /// <param name="value">The string to split into lines.</param>
        /// <param name="trimLines">If true the lines are trimmed.</param>
        public static IEnumerable<string> ToLines([AllowNull] this string value, bool trimLines)
        {
            if (value == null)
            {
                return Enumerable.Empty<string>();
            }

            var lines = new List<string>();
            var reader = new StringReader(value);

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }

                if (trimLines)
                {
                    lines.Add(line.Trim());
                }
                else
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
