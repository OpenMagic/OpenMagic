using System;

namespace OpenMagic.Extensions {
    public static class TypeExtensions {

        /// <summary>
        /// Return true if the type is a string.
        /// </summary>
        /// <param name="value">The type to test for string.</param>
        /// <remarks>
        /// Syntactic sugar.
        /// </remarks>
        public static bool IsString(this Type value) {
            return value == typeof(string);
        }
    }
}
