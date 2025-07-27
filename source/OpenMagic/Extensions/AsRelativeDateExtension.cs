using System;

namespace OpenMagic.Extensions
{
    public static class AsRelativeDateExtension
    {
        /// <summary>
        ///     Converts a string representation of a relative date into a <see cref="DateTime" /> object.
        /// </summary>
        /// <param name="value">The string representation of the relative date.</param>
        /// <returns>The <see cref="DateTime" /> object representing the relative date.</returns>
        public static DateTime AsRelativeDate(this string? value)
        {
            // Make a copy of the original value in case we need to throw an exception.
            var originalValue = value;

            try
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

                // If the value is "today" then return the current date.
                if (value != null && value.Equals("today", StringComparison.OrdinalIgnoreCase))
                {
                    return date;
                }

                // If the value does not start with "today" then try to parse it as a date.
                if (value != null && !value.StartsWith("today", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(value, out date))
                    {
                        return date;
                    }

                    throw new FormatException(value);
                }

                // Remove "today" from the start of the value.
                value = value?.Substring("today".Length).Trim();

                // Get whether to add or subtract.
                var operation = GetOperation(value);

                // Remove the operation from the start of the value.
                value = value?.Substring(1).TrimStart();

                value = HandleYears(value, operation, ref date);
                value = HandleMonths(value, operation, ref date);
                value = HandleDays(value, operation, ref date);

                if (value.IsNullOrWhiteSpace())
                {
                    return date;
                }

                throw new FormatException(value);
            }
            catch (FormatException exception)
            {
                throw CannotHandleRelativeDateException(originalValue, exception);
            }
        }

        private static ArgumentException CannotHandleRelativeDateException(string? value, FormatException innerException) => new($"Cannot handle relative date '{value}'.", nameof(value), innerException);

        private static string? HandleYears(string? value, int operation, ref DateTime date)
        {
            return HandleDateParts(value, operation, ref date, "year", (d, i) => d.AddYears(i));
        }

        private static string? HandleMonths(string? value, int operation, ref DateTime date)
        {
            return HandleDateParts(value, operation, ref date, "month", (d, i) => d.AddMonths(i));
        }

        private static string? HandleDays(string? value, int operation, ref DateTime date)
        {
            return HandleDateParts(value, operation, ref date, "day", (d, i) => d.AddDays(i));
        }


        private static string? HandleDateParts(string? value, int operation, ref DateTime date, string datePart, Func<DateTime, int, DateTime> changeDatePart)
        {
            // Must run plural version first
            value = HandleDatePart(value, operation, ref date, datePart + "s", changeDatePart);
            value = HandleDatePart(value, operation, ref date, datePart, changeDatePart);

            return value;
        }

        private static string? HandleDatePart(string? value, int operation, ref DateTime date, string datePart, Func<DateTime, int, DateTime> changeDatePart)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (value != null && (value.Contains(datePart + " ") || value.EndsWith(datePart)))
            {
                // Split the value into the part before the date part and the part after the date part.
                // e.g. "1 year and 2 months" would be split into "1" and "2 months".
                var values = value.Split([datePart], StringSplitOptions.RemoveEmptyEntries);

                if (!int.TryParse(values[0], out var adjustment))
                {
                    throw new FormatException(value);
                }

                date = changeDatePart(date, adjustment * operation);

                value = values.Length switch
                {
                    1 => null,
                    2 =>
                        // Remove any leading "and" and leading " " from the value.
                        values[1].TrimStart().TrimStart("and").TrimStart(),
                    _ => throw new FormatException(value)
                };
            }

            return value;
        }

        private static int GetOperation(string? value)
        {
            return value?.Substring(0, 1) switch
            {
                "+" => 1,
                "-" => -1,
                _ => throw new ArgumentException($@"Cannot handle relative date '{value}'.", nameof(value))
            };
        }
    }
}