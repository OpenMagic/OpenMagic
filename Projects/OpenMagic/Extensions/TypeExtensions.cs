using System;
using System.Collections.Generic;

namespace OpenMagic.Extensions {
    public static class TypeExtensions {

        /// <summary>
        /// Determines whether the specified type is IEnumerable<string>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        /// Syntactic sugar.
        /// </remarks>
        public static bool IsEnumerableString(this Type value) {
            return value == typeof(IEnumerable<string>);
        }

        /// <summary>
        /// Determines whether the specified type is string.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        /// Syntactic sugar.
        /// </remarks>
        public static bool IsString(this Type value) {
            return value == typeof(string);
        }
    }
}
