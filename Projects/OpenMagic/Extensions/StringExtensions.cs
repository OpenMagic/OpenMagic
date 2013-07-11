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
        /// Determines whether <paramref name="value"/> contains <paramref name="find"/>.
        /// </summary>
        /// <param name="value">The value to search.</param>
        /// <param name="find">The value to find.</param>
        /// <param name="stringComparison"><see cref="StringComparison"/>.</param>
        public static bool Contains(this string value, string find, StringComparison stringComparison)
        {
            // todo: unit tests
            switch (stringComparison)
            {

                case System.StringComparison.CurrentCultureIgnoreCase:
                case System.StringComparison.OrdinalIgnoreCase:

                    return value.ToLower().Contains(find.ToLower());

                case System.StringComparison.InvariantCultureIgnoreCase:

                    return value.ToLowerInvariant().Contains(find.ToLowerInvariant());
            }
            throw new ArgumentOutOfRangeException("stringComparison", string.Format("Value cannot be {0}.", stringComparison));
        }

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
            return (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()));
        }

        /// <summary>
        /// Get the text before <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>The text before <paramref name="value"/>. If <paramref name="value"/> does not exist then Nothing is returned.</returns>
        public static string TextBefore(this string text, string value)
        {
            // todo: unit tests
            return text.TextBefore(value, null);
        }

        /// <summary>
        /// Get the text before <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>The text before <paramref name="value"/>. If <paramref name="value"/> does not exist then <paramref name="defaultValue" /> is returned.</returns>
        public static string TextBefore(this string text, string value, string defaultValue)
        {
            // todo: unit tests
            value.MustNotBeNull("value");

            if (text.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            return text.TextBefore(text.IndexOf(value), defaultValue);
        }

        /// <summary>
        /// Get the text before <paramref name="value" />.
        /// </summary>
        /// <param name="value">The index position to retrieve text before.</param>
        /// <returns>The text before <paramref name="value"/> If <paramref name="value"/> is -1 then <paramref name="value" /> is returned.</returns>
        /// <remarks>This method is useful so you don't need to test the value of String.IndexOf() before calling substring string. eg
        /// "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextBefore("abc".IndexOf("d")) will return Nothing. An alternative
        /// for this example is "abc".TextBefore("d").
        /// </remarks>
        public static string TextBefore(this string text, int value, string defaultValue)
        {
            // todo: unit tests
            if (value == -1)
            {
                return defaultValue;
            }
            else
            {
                return text.Substring(0, value);
            }
        }

        /// <summary>
        /// Get the text before last occurrence of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <returns>The text before last occurrence of <paramref name="value"/>. If <paramref name="value"/> does not exist then Nothing is returned.</returns>
        public static string TextBeforeLast(this string text, string value)
        {
            // todo: unit tests
            return text.TextBeforeLast(value, null);
        }

        /// <summary>
        /// Get the text before last occurrence of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <returns>The text before last occurrence of <paramref name="value"/>. If <paramref name="value"/> does not exist then <paramref name="defaultValue" /> is returned.</returns>
        public static string TextBeforeLast(this string text, string value, string defaultValue)
        {
            // todo: unit tests
            value.MustNotBeNull("value");

            if (text.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            return text.TextBefore(text.LastIndexOf(value), defaultValue);
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
