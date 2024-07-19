using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

#pragma warning disable IDE0057

namespace OpenMagic.Extensions
{
    /// <summary>
    ///     Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts a string representation of a relative date into a <see cref="DateTime" /> object.
        /// </summary>
        /// <param name="value">The string representation of the relative date.</param>
        /// <returns>The <see cref="DateTime" /> object representing the relative date.</returns>
        public static DateTime AsRelativeDate(this string value)
        {
            switch (value)
            {
                case "DateTime.MaxValue":
                    return DateTime.MaxValue.Date;
                case "DateTime.MinValue":
                    return DateTime.MinValue.Date;
                case "DateTime.UtcNow":
                    return DateTime.UtcNow.Date;
            }

            var date = DateTime.UtcNow.Date;

            if (value.Equals("today", StringComparison.OrdinalIgnoreCase))
            {
                return DateTime.UtcNow.Date;
            }


            if (!value.StartsWith("today", StringComparison.OrdinalIgnoreCase))
            {
                if (DateTime.TryParse(value, out date))
                {
                    return date;
                }

                throw CannotHandleRelativeDateException(value);
            }

            var originalValue = value;

            value = value.Substring("today".Length).Trim();

            var operation = GetOperation(value);
            value = value.Substring(1).TrimStart();

            value = HandleMonths(value, operation, ref date, " months and ");
            value = HandleMonths(value, operation, ref date, " month and ");

            if (value.EndsWith("months", StringComparison.OrdinalIgnoreCase) || value.EndsWith("month", StringComparison.OrdinalIgnoreCase))
            {
                value = value.TrimEnd("months").TrimEnd("month");

                if (int.TryParse(value, out var addMonths))
                {
                    return date.AddMonths(addMonths * operation);
                }
            }

            if (value!.EndsWith("days", StringComparison.OrdinalIgnoreCase) || value.EndsWith("day", StringComparison.OrdinalIgnoreCase))
            {
                value = value.TrimEnd("days").TrimEnd("day");

                if (int.TryParse(value, out var addDays))
                {
                    return date.AddDays(addDays * operation);
                }
            }

            throw CannotHandleRelativeDateException(originalValue);
        }

        private static ArgumentException CannotHandleRelativeDateException(string value)
        {
            return new ArgumentException($"Cannot handle relative date '{value}'.", nameof(value));
        }

        private static string HandleMonths(string value, int operation, ref DateTime date, string monthsAnd)
        {
            if (value.Contains(monthsAnd))
            {
                var values = value.Split(monthsAnd, StringSplitOptions.RemoveEmptyEntries);
                var addMonths = int.Parse(values[0]);

                date = date.AddMonths(addMonths * operation);
                value = values[1];
            }

            return value;
        }

        private static int GetOperation(string value)
        {
            return value.Substring(0, 1) switch
            {
                "+" => 1,
                "-" => -1,
                _ => throw new ArgumentException($@"Cannot handle relative date '{value}'.", nameof(value))
            };
        }

        /// <summary>
        ///     Get the values in a string that are between a pair of delimiters.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <param name="delimiter">The value delimiter.</param>
        /// <example>
        ///     GetValuesBetween("a 'quick' brown 'fox'") => { "quick", "fox }.
        /// </example>
        public static IEnumerable<string> GetValuesBetween([AllowNull] this string value, string delimiter)
        {
            // todo: replace with Argument.MustNotBeNull() or similar.
            if (delimiter.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Value cannot be whitespace.", nameof(delimiter));
            }

            if (delimiter.Length > 1)
            {
                throw new ArgumentException("Value cannot be longer than 1 character.", nameof(delimiter));
            }

            if (value == null)
            {
                return Enumerable.Empty<string>();
            }

            var split = value.Split(Convert.ToChar(delimiter));
            var values = new List<string>();

            for (var i = 1; i < split.Length; i += 2)
            {
                // todo: yield return split[i] should work, but it breaks String.IsNullOrWhiteSpace(delimiter) test.
                values.Add(split[i]);
            }

            return values;
        }

        /// <summary>
        ///     Inserts a string before each upper case character in a string.
        /// </summary>
        /// <param name="value">The value to insert <paramref name="insert" /> into.</param>
        /// <param name="insert">The string to insert into <paramref name="value" />.</param>
        /// <example>
        ///     See OpenMagic.Specifications.Features.Extensions.InsertStringBeforeEachUpperCaseCharacter.feature.
        /// </example>
        public static string InsertStringBeforeEachUpperCaseCharacter(this string value, string insert)
        {
            var sb = new StringBuilder(value.Length + insert.Length * (value.Length - 1));

            sb.Append(value[0]);

            for (var i = 1; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    sb.Append(insert);
                }

                sb.Append(value[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        ///     Syntactic sugar.
        /// </remarks>
        public static bool IsNullOrWhiteSpace([AllowNull] this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     Tests if
        ///     <param name="emailAddress" />
        ///     is a valid email address.
        /// </summary>
        /// <param name="emailAddress">The value to test.</param>
        /// <returns>
        ///     True is
        ///     <param name="emailAddress" />
        ///     is a valid email address.
        /// </returns>
        /// <remarks>
        ///     Copied from https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address.
        /// </remarks>
        public static bool IsValidEmailAddress(this string emailAddress)
        {
            emailAddress.MustNotBeNullOrWhiteSpace(nameof(emailAddress));

            var trimmedEmail = emailAddress.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }

            try
            {
                var mailAddress = new MailAddress(emailAddress);
                return mailAddress.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Normalizes the line endings within a string.
        /// </summary>
        public static string NormalizeLineEndings(this string value)
        {
            return value.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        ///     Returns the number of times <paramref name="find" /> occurs in <paramref name="value" />.
        /// </summary>
        /// <exception cref="ArgumentNullException"> when find is null or empty.</exception>
        public static int Occurs(this string value, string find)
        {
            find.MustNotBeNullOrWhiteSpace(nameof(find));

            return value == null ? 0 : Regex.Matches(value, Regex.Escape(find)).Count;
        }

        /// <summary>
        ///     Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The index position to retrieve text after.</param>
        /// <returns>The text after <paramref name="value" /> If <paramref name="value" /> is -1 then Nothing is returned.</returns>
        /// <remarks>
        ///     This method is useful, so you don't need to test the value of String.IndexOf() before calling substring string. eg
        ///     "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextAfter("abc".IndexOf("d")) will return
        ///     Nothing. An alternative
        ///     for this example is "abc".TextAfter("d").
        /// </remarks>
        public static string TextAfter(this string text, int value)
        {
            return text.TextAfter(value, null);
        }

        /// <summary>
        ///     Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The index position to retrieve text after.</param>
        /// <param name="defaultValue">Value to return if <paramref name="value" /> is -1.</param>
        /// <returns>
        ///     The text after <paramref name="value" /> If <paramref name="value" /> is -1 then
        ///     <paramref name="defaultValue" /> is returned.
        /// </returns>
        /// <remarks>
        ///     This method is useful, so you don't need to test the value of String.IndexOf() before calling substring string. eg
        ///     "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextAfter("abc".IndexOf("d")) will return
        ///     Nothing. An alternative
        ///     for this example is "abc".TextAfter("d").
        /// </remarks>
        public static string TextAfter(this string text, int value, [AllowNull] string defaultValue)
        {
            return text.TextAfter(value, defaultValue, 1);
        }

        /// <summary>
        ///     Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The index position to retrieve text after.</param>
        /// <param name="defaultValue">Value to return if <paramref name="value" /> is -1.</param>
        /// <param name="offset">Index to start search from.</param>
        /// <returns>
        ///     The text after <paramref name="value" /> If <paramref name="value" /> is -1 then
        ///     <paramref name="defaultValue" /> is returned.
        /// </returns>
        /// <remarks>
        ///     This method is useful, so you don't need to test the value of String.IndexOf() before calling substring string. eg
        ///     "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextAfter("abc".IndexOf("d")) will return
        ///     Nothing. An alternative
        ///     for this example is "abc".TextAfter("d").
        /// </remarks>
        public static string TextAfter(this string text, int value, [AllowNull] string defaultValue, int offset)
        {
            return value == -1 ? defaultValue : text[(value + offset)..];
        }

        /// <summary>
        ///     Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>
        ///     The text after <paramref name="value" />. If <paramref name="value" /> does not exist then Nothing is
        ///     returned.
        /// </returns>
        public static string TextAfter(this string text, string value)
        {
            // todo: unit tests
            return text.TextAfter(value, null);
        }

        /// <summary>
        ///     Get the text after <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <param name="defaultValue">The value to return if text is null or white space.</param>
        /// <returns>
        ///     The text after <paramref name="value" />. If <paramref name="value" /> does not exist then
        ///     <paramref name="defaultValue" /> is returned.
        /// </returns>
        public static string TextAfter(this string text, string value, [AllowNull] string defaultValue)
        {
            value.MustNotBeNullOrWhiteSpace("value");

            if (text.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }

            var index = text.IndexOf(value, StringComparison.Ordinal);

            if (index > -1)
            {
                index += value.Length - 1;
            }

            return text.TextAfter(index, defaultValue);
        }

        /// <summary>
        ///     Get the text after last occurrence of <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <returns>
        ///     The text after last occurrence of <paramref name="value" />. If <paramref name="value" /> does not exist then
        ///     Nothing is returned.
        /// </returns>
        public static string TextAfterLast(this string text, string value)
        {
            // todo: unit tests
            return text.TextAfterLast(value, null);
        }

        /// <summary>
        ///     Get the text after last occurrence of <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <param name="defaultValue">
        ///     Value to return if <paramref name="value" /> does not exist in <paramref name="text" />
        /// </param>
        /// <returns>
        ///     The text after last occurrence of <paramref name="value" />. If <paramref name="value" /> does not exist then
        ///     <paramref name="text" /> is returned.
        /// </returns>
        public static string TextAfterLast(this string text, string value, [AllowNull] string defaultValue)
        {
            return text.IsNullOrWhiteSpace() ? defaultValue : text.TextAfter(text.LastIndexOf(value, StringComparison.Ordinal), defaultValue, value.Length);
        }

        /// <summary>
        ///     Get the text before <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <returns>
        ///     The text before <paramref name="value" />. If <paramref name="value" /> does not exist then Nothing is
        ///     returned.
        /// </returns>
        public static string TextBefore(this string text, string value)
        {
            // todo: unit tests
            return text.TextBefore(value, null);
        }

        /// <summary>
        ///     Get the text before <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown is null or empty.</param>
        /// <param name="defaultValue">
        ///     Value to return if <paramref name="value" /> does not exist in <paramref name="text" />
        /// </param>
        /// <returns>
        ///     The text before <paramref name="value" />. If <paramref name="value" /> does not exist then
        ///     <paramref name="defaultValue" /> is returned.
        /// </returns>
        public static string TextBefore(this string text, string value, [AllowNull] string defaultValue)
        {
            return text.IsNullOrWhiteSpace() ? defaultValue : text.TextBefore(text.IndexOf(value, StringComparison.Ordinal), defaultValue);
        }

        /// <summary>
        ///     Get the text before <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The index position to retrieve text before.</param>
        /// <param name="defaultValue">
        ///     Value to return if <paramref name="value" /> does not exist in <paramref name="text" />
        /// </param>
        /// <returns>
        ///     The text before <paramref name="value" /> If <paramref name="value" /> is -1 then <paramref name="value" /> is
        ///     returned.
        /// </returns>
        /// <remarks>
        ///     This method is useful, so you don't need to test the value of String.IndexOf() before calling substring string. eg
        ///     "abc".Substring("abc".IndexOf("d")) would raise an exception. "abc".TextBefore("abc".IndexOf("d")) will return
        ///     Nothing. An alternative
        ///     for this example is "abc".TextBefore("d").
        /// </remarks>
        public static string TextBefore(this string text, int value, [AllowNull] string defaultValue)
        {
            // todo: unit tests
            return value == -1 ? defaultValue : text[..value];
        }

        /// <summary>
        ///     Get the text before last occurrence of <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <returns>
        ///     The text before last occurrence of <paramref name="value" />. If <paramref name="value" /> does not exist then
        ///     Nothing is returned.
        /// </returns>
        public static string TextBeforeLast(this string text, string value)
        {
            return text.TextBeforeLast(value, null);
        }

        /// <summary>
        ///     Get the text before last occurrence of <paramref name="value" />.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <param name="value">The string to find. ArgumentNullException thrown if null or empty.</param>
        /// <param name="defaultValue">
        ///     Value to return if <paramref name="value" /> does not exist in <paramref name="text" />
        /// </param>
        /// <returns>
        ///     The text before last occurrence of <paramref name="value" />. If <paramref name="value" /> does not exist then
        ///     <paramref name="defaultValue" /> is returned.
        /// </returns>
        public static string TextBeforeLast(this string text, string value, [AllowNull] string defaultValue)
        {
            return text.IsNullOrWhiteSpace() ? defaultValue : text.TextBefore(text.LastIndexOf(value, StringComparison.Ordinal), defaultValue);
        }

        /// <summary>
        ///     Splits a string value into lines.
        /// </summary>
        /// <param name="value">The string to split into lines.</param>
        /// <param name="trimLines">If true the lines are trimmed.</param>
        public static IEnumerable<string> ToLines([AllowNull] this string value, bool trimLines = false)
        {
            if (value == null)
            {
                yield break;
            }

            using (var reader = new StringReader(value))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return trimLines ? line.Trim() : line;
                }
            }
        }

        /// <summary>
        ///     Naive implementation of converting a string to a NameValueCollection.
        /// </summary>
        /// <param name="value">The value to convert. Example: a=1;b=2.</param>
        public static NameValueCollection ToNameValueCollection(this string value)
        {
            var collection = new NameValueCollection();
            value.Split(';')
                .Select(part => part.Split('='))
                .ToList()
                .ForEach(pair => collection.Add(pair[0], pair.Length > 1 ? pair[1] : null));

            return collection;
        }

        /// <summary>
        ///     Removes all occurrences of a specified string from the end of the current String object.
        /// </summary>
        /// <param name="value">The string to remove <paramref name="trimString" /> from.</param>
        /// <param name="trimString">The string to be removed from the end of the current String object.</param>
        /// <returns>
        ///     <paramref name="value" /> is returned if it does not end with <paramref name="trimString" />. Otherwise,
        ///     <paramref name="value" /> without trailing <paramref name="trimString" /> is returned.
        /// </returns>
        /// <example>
        ///     See OpenMagic.Specifications.Features.Extensions.TrimEnd.feature.
        /// </example>
        public static string TrimEnd(this string value, string trimString)
        {
            while (value.EndsWith(trimString))
            {
                value = value.Substring(0, value.Length - trimString.Length);
            }

            return value;
        }

        /// <summary>
        ///     Removes all occurrences of a specified string from the start of the current String object.
        /// </summary>
        /// <param name="value">The string to remove <paramref name="trimString" /> from.</param>
        /// <param name="trimString">The string to be removed from the start of the current String object.</param>
        /// <returns>
        ///     <paramref name="value" /> is returned if it does not start with <paramref name="trimString" />. Otherwise,
        ///     <paramref name="value" /> without leading <paramref name="trimString" /> is returned.
        /// </returns>
        /// <example>
        ///     See OpenMagic.Specifications.Features.Extensions.TrimStart.feature.
        /// </example>
        public static string TrimStart(this string value, string trimString)
        {
            while (value.StartsWith(trimString))
            {
                value = value.Substring(trimString.Length);
            }

            return value;
        }

        /// <summary>
        ///     Splits a string into lines and writes them to a <see cref="TextWriter" />.
        /// </summary>
        /// <param name="value">The string value to split into lines.</param>
        /// <param name="textWriter">The <see cref="TextWriter" /> to write to.</param>
        /// <param name="trimLines">When true the lines are trimmed before writing to <see cref="TextWriter" />.</param>
        public static void WriteLines([AllowNull] this string value, TextWriter textWriter, bool trimLines = false)
        {
            if (value == null)
            {
                return;
            }

            foreach (var line in value.ToLines(trimLines))
            {
                textWriter.WriteLine(line);
            }
        }
    }
}