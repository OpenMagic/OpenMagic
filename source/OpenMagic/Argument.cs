using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NullGuard;
using OpenMagic.Exceptions;
using OpenMagic.Extensions;

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
        public static T Must<T>(this T param, bool assertionResult, string message, string paramName)
        {
            if (!assertionResult)
            {
                throw new ArgumentException(message, paramName);
            }

            return param;
        }

        public static string MustBeAnEmailAddress(this string emailAddress, string paramName)
        {
            emailAddress.MustNotBeNullOrWhiteSpace(nameof(emailAddress));
            paramName.MustNotBeNullOrWhiteSpace(nameof(paramName));

            if (!emailAddress.IsValidEmailAddress())
            {
                throw new ArgumentNotValidEmailAddress(paramName, emailAddress);
            }

            return emailAddress;
        }

        public static T MustBeGreaterThan<T>(this T param, T greaterThan, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(greaterThan) > 0)
            {
                return param;
            }

            var exception = new ArgumentOutOfRangeException(paramName, string.Format("Value must be greater than {0}.", greaterThan));

            exception.Data.Add("param", param);
            exception.Data.Add("greaterThan", greaterThan);
            exception.Data.Add("paramName", paramName);

            throw exception;
        }

        public static T MustBeGreaterThanOrEqualTo<T>(this T param, T greaterThanOrEqualTo, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(greaterThanOrEqualTo) >= 0)
            {
                return param;
            }

            var exception = new ArgumentOutOfRangeException(paramName, $"Value must be greater than or equal to {greaterThanOrEqualTo}.");

            exception.Data.Add(nameof(param), param);
            exception.Data.Add(nameof(greaterThanOrEqualTo), greaterThanOrEqualTo);
            exception.Data.Add(nameof(paramName), paramName);

            throw exception;
        }

        public static T MustBeBetween<T>(this T param, T minimumValue, T maximumValue, string paramName) where T : IComparable<T>
        {
            if (param.CompareTo(minimumValue) >= 0 && param.CompareTo(maximumValue) <= 0)
            {
                return param;
            }

            var exception = new ArgumentOutOfRangeException(paramName, string.Format("Value must be between {0} and {1}.", minimumValue, maximumValue));

            exception.Data.Add("param", param);
            exception.Data.Add("minimumValue", minimumValue);
            exception.Data.Add("maximumValue", maximumValue);
            exception.Data.Add("paramName", paramName);

            throw exception;
        }

        public static T[] MustNotBeEmpty<T>(this T[] param, string paramName)
        {
            // todo: test & document
            if (!param.Any())
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
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
                throw new ArgumentException("Value cannot be empty.", paramName);
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
        public static IEnumerable<T> MustNotBeNullOrEmpty<T>(this IEnumerable<T> param, string paramName)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            if (!param.Any())
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }

            // ReSharper disable once PossibleMultipleEnumeration
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
            param.MustNotBeNull(paramName);

            if (param.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Value cannot be whitespace.", paramName);
            }

            return param;
        }
    }
}