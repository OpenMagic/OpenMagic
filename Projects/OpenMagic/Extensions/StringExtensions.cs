using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMagic.Extensions {
    public static class StringExtensions {

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        /// Syntactic sugar.
        /// </remarks>
        public static bool IsNullOrWhiteSpace(this string value) {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
