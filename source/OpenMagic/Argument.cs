using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using OpenMagic.Exceptions;
using OpenMagic.Extensions;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace OpenMagic
{
    /// <summary>
    ///     Collection of argument testing methods.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when the <paramref name="param">directory</paramref> does not exist.
        /// </summary>
        /// <param name="param">
        ///     The directory to test existence of.
        /// </param>
        /// <param name="paramName">
        ///     Name of the parameter.
        /// </param>
        /// <returns>
        ///     Returns <paramref name="param" /> when the directory exists.
        /// </returns>
        public static DirectoryInfo DirectoryMustExist(DirectoryInfo param, string paramName)
        {
            if (!param.Exists)
            {
                throw new ArgumentException("Directory must exist.", paramName, new DirectoryNotFoundException("Cannot find directory."));
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when the <paramref name="param">file</paramref> does not exist.
        /// </summary>
        /// <param name="param">
        ///     The file to test existence of.
        /// </param>
        /// <param name="paramName">
        ///     Name of the parameter.
        /// </param>
        /// <returns>
        ///     Returns <paramref name="param" /> when the file exists.
        /// </returns>
        public static FileInfo FileMustExist(FileInfo param, string paramName)
        {
            if (!param.Exists)
            {
                throw new ArgumentException("File must exist.", paramName, new FileNotFoundException("Cannot find file.", param.FullName));
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when assertion on <paramref name="param" /> has failed.
        /// </summary>
        /// <typeparam name="T">The <paramref name="param" /> type.</typeparam>
        /// <param name="param">The value that was tested.</param>
        /// <param name="assertionResult">The result of the assertion on <paramref name="param" />.</param>
        /// <param name="message">The exception message to use when <paramref name="assertionResult" /> is false.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not null.</returns>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        public static T Must<T>(this T param, bool assertionResult, string message, string paramName)
        {
            if (!assertionResult)
            {
                throw new ArgumentException(message, paramName);
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when the <paramref name="emailAddress">email address</paramref> is not a valid email address.
        /// </summary>
        /// <param name="emailAddress">
        ///     The email address to validate.
        /// </param>
        /// <param name="paramName">
        ///     Name of the parameter.
        /// </param>
        /// <returns>
        ///     Returns <paramref name="emailAddress" /> when it is a valid email address.
        /// </returns>
        public static string MustBeAnEmailAddress(this string emailAddress, string paramName)
        {
            emailAddress.MustNotBeNullOrWhiteSpace(nameof(emailAddress));

            if (!emailAddress.IsValidEmailAddress())
            {
                throw new ArgumentNotAnEmailAddressException(paramName, emailAddress);
            }

            return emailAddress;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when the <paramref name="param">value</paramref> is not greater than <paramref name="greaterThan" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="param">The value to test.</param>
        /// <param name="greaterThan">The value to compare against.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>
        ///     Returns <paramref name="param" /> when the value is greater than <paramref name="greaterThan" />.
        /// </returns>
        public static T MustBeGreaterThan<T>(this T param, T greaterThan, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(greaterThan) > 0)
            {
                return param;
            }

            var exception = new ArgumentOutOfRangeException(paramName, $"Value must be greater than {greaterThan}.")
            {
                Data =
                {
                    [nameof(param)] = param,
                    [nameof(greaterThan)] = greaterThan,
                    [nameof(paramName)] = paramName
                }
            };


            throw exception;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when the <paramref name="param">value</paramref> is not greater than or equal to <paramref name="greaterThanOrEqualTo" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="param">The value to test.</param>
        /// <param name="greaterThanOrEqualTo">The value to compare against.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>
        ///     Returns <paramref name="param" /> when the value is greater than or equal to <paramref name="greaterThanOrEqualTo" />.
        /// </returns>
        public static T MustBeGreaterThanOrEqualTo<T>(this T param, T greaterThanOrEqualTo, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(greaterThanOrEqualTo) >= 0)
            {
                return param;
            }

            var exception = new ArgumentOutOfRangeException(paramName, $"Value must be greater than or equal to {greaterThanOrEqualTo}.")
            {
                Data =
                {
                    [nameof(param)] = param,
                    [nameof(greaterThanOrEqualTo)] = greaterThanOrEqualTo,
                    [nameof(paramName)] = paramName
                }
            };

            throw exception;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when the <paramref name="param">value</paramref> is not less than or equal to <paramref name="lessThanOrEqualTo" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="param">The value to test.</param>
        /// <param name="lessThanOrEqualTo">The value to compare against.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>
        ///     Returns <paramref name="param" /> when the value is less than or equal to <paramref name="lessThanOrEqualTo" />.
        /// </returns>
        public static T MustBeLessThanOrEqualTo<T>(this T param, T lessThanOrEqualTo, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(lessThanOrEqualTo) <= 0)
            {
                return param;
            }

            var lessThanOrEqualToString = typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?)
                ? ((DateTime)(object)lessThanOrEqualTo).ToString("d MMM yyyy")
                : lessThanOrEqualTo.ToString();

            var exception = new ArgumentOutOfRangeException(paramName, $"Value must be less than or equal to {lessThanOrEqualToString}.")
                {
                Data =
                {
                    [nameof(param)] = param,
                    [nameof(lessThanOrEqualTo)] = lessThanOrEqualTo,
                    [nameof(paramName)] = paramName
                }
            };

            throw exception;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentOutOfRangeException" /> when the <paramref name="param">value</paramref> is not between <paramref name="minimumValue" /> and <paramref name="maximumValue" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="param">The value to test.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>
        ///     Returns <paramref name="param" /> when the value is between <paramref name="minimumValue" /> and <paramref name="maximumValue" />.
        /// </returns>
        public static T MustBeBetween<T>(this T param, T minimumValue, T maximumValue, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(minimumValue) >= 0 && param.CompareTo(maximumValue) <= 0)
            {
                return param;
            }

            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {minimumValue} and {maximumValue}.")
            {
                Data =
                {
                    [nameof(param)] = param,
                    [nameof(minimumValue)] = minimumValue,
                    [nameof(maximumValue)] = maximumValue,
                    [nameof(paramName)] = paramName
                }
            };
        }

        /// <summary>
        ///     Throws <see cref="ArgumentEmptyException" /> when the <paramref name="param" /> is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the <paramref name="param" />.</typeparam>
        /// <param name="param">The enumerable to test for emptiness.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when it is not empty.</returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<T> MustNotBeEmpty<T>(this IEnumerable<T> param, string paramName)
        {
            if (!param.Any())
            {
                throw new ArgumentEmptyException(paramName);
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentException" /> when <paramref name="param" /> is <see cref="Guid.Empty" />.
        /// </summary>
        /// <param name="param">The value to test for <see cref="Guid.Empty" />.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not <see cref="Guid.Empty" />.</returns>
        /// <exception cref="ArgumentException">when <paramref name="param" /> is <see cref="Guid.Empty" /></exception>
        public static Guid MustNotBeEmpty(this Guid param, string paramName)
        {
            if (Guid.Empty.Equals(param))
            {
                throw new ArgumentEmptyException(paramName);
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentNullException" /> when <paramref name="param" /> is null or
        ///     <see cref="ArgumentEmptyException" /> when <paramref name="param" /> is empty.
        /// </summary>
        /// <param name="param">The value to test for null or empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not null.</returns>
        public static string MustNotBeEmpty(this string param, string paramName)
        {
            if (param is "")
            {
                throw new ArgumentEmptyException(paramName);
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentWhiteSpaceException" /> when <paramref name="param" /> is not null or empty, but contains only whitespace.
        /// </summary>
        /// <param name="param">The value to test for whitespace.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not null or empty, but contains only whitespace.</returns>
        public static string MustNotBeWhiteSpace(this string param, string paramName)
        {
            if (!string.IsNullOrEmpty(param) && param.IsNullOrWhiteSpace())
            {
                throw new ArgumentWhiteSpaceException(paramName);
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentNullException" /> when <paramref name="param" /> is null.
        /// </summary>
        /// <typeparam name="T">The <paramref name="param" /> type.</typeparam>
        /// <param name="param">The value to test for null.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not null.</returns>
        public static T MustNotBeNull<T>([AllowNull] this T param, string paramName)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentNullException" /> when <paramref name="param" /> is null or
        ///     <see cref="ArgumentException" /> when <paramref name="param" /> is empty.
        /// </summary>
        /// <typeparam name="T">The <paramref name="param" /> type.</typeparam>
        /// <param name="param">The value to test for null or empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not null.</returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<T> MustNotBeNullOrEmpty<T>([AllowNull] this IEnumerable<T> param, string paramName)
        {
            param.MustNotBeNull(paramName);
            param.MustNotBeEmpty(paramName);

            return param;
        }

        /// <summary>
        ///     Throws <see cref="ArgumentNullException" /> when <paramref name="param" /> is null or
        ///     <see cref="ArgumentException" /> when <paramref name="param" /> is whitespace.
        /// </summary>
        /// <param name="param">The value to test for null or whitespace.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param" /> when the value is not null or whitespace.</returns>
        public static string MustNotBeNullOrWhiteSpace([AllowNull] this string param, string paramName)
        {
            if (param.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullOrWhiteSpaceException(paramName);
            }

            return param;
        }
    }
}