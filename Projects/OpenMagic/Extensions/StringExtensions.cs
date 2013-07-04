using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Logging;

namespace OpenMagic.Extensions
{
    public static class StringExtensions
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get the values in a string that are between a pair of delimiters.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <param name="delimiter">The value delimiter.</param>
        /// <example>
        /// GetValuesBetween("a 'quick' brown 'fox'") => { "quick", "fox }.
        /// </example>
        public static IEnumerable<string> GetValuesBetween(this string value, string delimiter)
        {
            log.Trace(m => m("GetValuesBetween(value: '{0}', delimiter: '{1}')", value, delimiter));

            // todo: replace with Argument.MustNotBeNull() or similar.
            if (delimiter == null) { throw new ArgumentNullException("delimiter"); }
            if (delimiter.IsNullOrWhiteSpace()) { throw new ArgumentException("Value cannot be whitespace.", "delimiter"); }
            if (delimiter.Length > 1) { throw new ArgumentException("Value cannot be longer than 1 character.", "delimiter"); }

            if (value == null)
            {
                return Enumerable.Empty<string>();
            }

            var split = value.Split(Convert.ToChar(delimiter));
            var values = new List<string>();

            for (int i = 1; i < split.Count(); i = i + 2)
            {
                // todo: yield return split[i] should work but it breaks String.IsNullOrWhiteSpace(delimiter) test.
                values.Add(split[i]);
            }

            return values;
        }

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
        /// <param name="trimLines">If true the lines are trimmed.</param>
        public static IEnumerable<string> ToLines(this string value, bool trimLines = false)
        {
            if (value == null)
            {
                yield break;
            }

            var reader = new StringReader(value);

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null)
                {
                    yield break;
                }

                if (trimLines)
                {
                    yield return line.Trim();
                }
                else
                {
                    yield return line;
                }
            }
        }

        /// <summary>
        /// Splits a string into lines and writes them to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="value">The string value to split into lines.</param>
        /// <param name="textWriter">The <see cref="TextWriter"/> to write to.</param>
        /// <param name="trimLines">When true the lines are trimmed before writing to <see cref="TextWriter"/>.</param>
        public static void WriteLines(this string value, TextWriter textWriter, bool trimLines = false)
        {
            Argument.MustNotBeNull(textWriter, "textWriter");

            if (value == null)
            {
                return;
            }

            foreach (var line in value.ToLines(trimLines: trimLines))
            {
                textWriter.WriteLine(line);
            }
        }
    }
}
