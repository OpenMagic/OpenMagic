using System;
using System.Collections.Generic;
using System.Linq;
using OpenMagic.Extensions;

namespace OpenMagic
{
    /// <summary>
    /// Collection of argument testing methods.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when <paramref name="param"/> is null.
        /// </summary>
        /// <typeparam name="T">The <paramref name="param"/> type.</typeparam>
        /// <param name="param">The value to test for null.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param"/> when the value is not null.</returns>
        public static T MustNotBeNull<T>(T param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return param;
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when <paramref name="param"/> is null or <see cref="ArgumentException"/> when <paramref name="param"/> is empty.
        /// </summary>
        /// <typeparam name="T">The <paramref name="param"/> type.</typeparam>
        /// <param name="param">The value to test for null or empty.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param"/> when the value is not null.</returns>
        public static IEnumerable<T> MustNotBeNullOrEmpty<T>(IEnumerable<T> param, string paramName)
        {
            Argument.MustNotBeNull(param, paramName);

            if (!param.Any())
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }

            return param;
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when <paramref name="param"/> is null or <see cref="ArgumentException"/> when <paramref name="param"/> is whitespace.
        /// </summary>
        /// <typeparam name="T">The <paramref name="param"/> type.</typeparam>
        /// <param name="param">The value to test for null or whitespace.</param>
        /// <param name="paramName">The name of the parameter being tested.</param>
        /// <returns>Returns <paramref name="param"/> when the value is not null or whitespace.</returns>
        public static string MustNotBeNullOrWhiteSpace(string param, string paramName)
        {
            Argument.MustNotBeNull(param, paramName);

            if (param.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Value cannot be whitespace.", paramName);
            }

            return param;
        }
    }
}
