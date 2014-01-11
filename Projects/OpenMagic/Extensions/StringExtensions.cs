using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NullGuard;

namespace OpenMagic.Extensions
{
    public static class StringExtensions
    {
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
        public static IEnumerable<string> GetValuesBetween([AllowNull] this string value, string delimiter)
        {
            // todo: replace with Argument.MustNotBeNull() or similar.
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
        public static bool IsNullOrWhiteSpace([AllowNull] this string value)
        {
            return (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()));
        }

        /// <summary>
        /// Returns the number of times <paramref name="find"/> occurs in <paramref name="value"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> when find is null or empty.</exception>
        public static int Occurs(this string value, string find)
        {
            // todo: unit tests
            find.MustNotBeNullOrEmpty("find");

            if (value == null)
            {
                return 0;
            }

            return Regex.Matches(value, Regex.Escape(find)).Count;
        }

        /// <summary>
        /// Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="value">The index position to retrieve text after.</param>
        /// <returns>The text after <paramref name="value"/> If <paramref name="value"/> is -1 then Nothing is returned.</returns>
        /// <remarks>This method is useful so you don't need to test the value of String.IndexOf() before calling substring string. eg
        /// "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextAfter("abc".IndexOf("d")) will return Nothing. An alternative
        /// for this example is "abc".TextAfter("d").
        /// </remarks>
        [return: AllowNull]
        public static string TextAfter(this string text, int value)
        {
            // todo: unit tests
            return text.TextAfter(value, null);
        }

        /// <summary>
        /// Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="value">The index position to retrieve text after.</param>
        /// <param name="defaultValue">Value to return if <paramref name="value" /> is -1.</param>
        /// <returns>The text after <paramref name="value"/> If <paramref name="value"/> is -1 then <paramref name="defaultValue" /> is returned.</returns>
        /// <remarks>This method is useful so you don't need to test the value of String.IndexOf() before calling substring string. eg
        /// "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextAfter("abc".IndexOf("d")) will return Nothing. An alternative
        /// for this example is "abc".TextAfter("d").
        /// </remarks>
        [return: AllowNull]
        public static string TextAfter(this string text, int value, [AllowNull] string defaultValue)
        {
            return text.TextAfter(value, defaultValue, 1);
        }

        /// <summary>
        /// Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="value">The index position to retrieve text after.</param>
        /// <param name="defaultValue">Value to return if <paramref name="value" /> is -1.</param>
        /// <returns>The text after <paramref name="value"/> If <paramref name="value"/> is -1 then <paramref name="defaultValue" /> is returned.</returns>
        /// <remarks>This method is useful so you don't need to test the value of String.IndexOf() before calling substring string. eg
        /// "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextAfter("abc".IndexOf("d")) will return Nothing. An alternative
        /// for this example is "abc".TextAfter("d").
        /// </remarks>
        [return: AllowNull]
        public static string TextAfter(this string text, int value, [AllowNull] string defaultValue, int offset)
        {
            // todo: unit tests
            if (value == -1)
            {
                return defaultValue;
            }
            else
            {
                return text.Substring(value + offset);
            }
        }

        /// <summary>
        /// Get the text after <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>The text after <paramref name="value"/>. If <paramref name="value"/> does not exist then Nothing is returned.</returns>
        [return: AllowNull]
        public static string TextAfter(this string text, string value)
        {
            // todo: unit tests
            return text.TextAfter(value, null);
        }

        /// <summary>
        /// Get the text after <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>The text after <paramref name="value"/>. If <paramref name="value"/> does not exist then <paramref name="defaultValue" /> is returned.</returns>
        [return: AllowNull]
        public static string TextAfter(this string text, string value, [AllowNull] string defaultValue)
        {
            // todo: unit tests
            int index = 0;

            value.MustNotBeNullOrWhiteSpace("value");

            if (text.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            index = text.IndexOf(value);

            if (index > -1)
            {
                index += value.Length - 1;
            }

            return text.TextAfter(index, defaultValue);
        }

        /// <summary>
        /// Get the text after last occurrence of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <returns>The text after last occurrence of <paramref name="value"/>. If <paramref name="value"/> does not exist then Nothing is returned.</returns>
        [return: AllowNull]
        public static string TextAfterLast(this string text, string value)
        {
            // todo: unit tests
            return text.TextAfterLast(value, null);
        }

        /// <summary>
        /// Get the text after last occurrence of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <param name="defaultValue">Value to return if <paramref name="value" /> does not exist in <paramref name="text" /></param>
        /// <returns>The text after last occurrence of <paramref name="value"/>. If <paramref name="value"/> does not exist then <paramref name="text"/> is returned.</returns>
        [return: AllowNull]
        public static string TextAfterLast(this string text, string value, [AllowNull] string defaultValue)
        {
            // todo: unit tests
            if (text.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            return text.TextAfter(text.LastIndexOf(value), defaultValue, value.Length);
        }

        /// <summary>
        /// Get the text before <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>The text before <paramref name="value"/>. If <paramref name="value"/> does not exist then Nothing is returned.</returns>
        [return: AllowNull]
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
        [return: AllowNull]
        public static string TextBefore(this string text, string value, [AllowNull] string defaultValue)
        {
            // todo: unit tests
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
        [return: AllowNull]
        public static string TextBefore(this string text, int value, [AllowNull] string defaultValue)
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
        [return: AllowNull]
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
        [return: AllowNull]
        public static string TextBeforeLast(this string text, string value, [AllowNull] string defaultValue)
        {
            // todo: unit tests
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
        public static IEnumerable<string> ToLines([AllowNull] this string value, bool trimLines = false)
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
        public static void WriteLines([AllowNull] this string value, TextWriter textWriter, bool trimLines = false)
        {
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
