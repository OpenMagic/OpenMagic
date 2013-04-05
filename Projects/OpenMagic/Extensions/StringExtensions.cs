using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenMagic.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        /// Syntactic sugar.
        /// </remarks>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Splits a string value into lines.
        /// </summary>
        /// <param name="value">The string to split into lines.</param>
        public static IEnumerable<string> ToLines(this string value)
        {
            return value.ToLines(false);
        }

        /// <summary>
        /// Splits a string value into lines.
        /// </summary>
        /// <param name="value">The string to split into lines.</param>
        /// <param name="trimLines">If true the lines are trimmed.</param>
        public static IEnumerable<string> ToLines(this string value, bool trimLines)
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
