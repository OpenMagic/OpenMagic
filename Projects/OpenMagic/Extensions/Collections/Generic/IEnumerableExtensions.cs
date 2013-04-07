using System.Collections.Generic;
using System.Linq;
using NullGuard;

namespace OpenMagic.Extensions.Collections.Generic {
    public static class IEnumerableExtensions {

        /// <summary>
        /// Indicates whether a specified enumerable is null or empty.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public static bool IsNullOrEmpty<T>([AllowNull] this IEnumerable<T> value) {
            return value == null || !value.Any();
        }
    }
}
