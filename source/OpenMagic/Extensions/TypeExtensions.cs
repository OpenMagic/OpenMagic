using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenMagic.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Determines whether the specified type is IEnumerable of <string />.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        ///     Syntactic sugar.
        /// </remarks>
        public static bool IsEnumerableString(this Type value)
        {
            return value == typeof(IEnumerable<string>);
        }

        /// <summary>
        ///     Determines whether the specified type is string.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <remarks>
        ///     Syntactic sugar.
        /// </remarks>
        public static bool IsString(this Type value)
        {
            return value == typeof(string);
        }

        // ReSharper disable once MemberCanBePrivate.Global because this is a part of the public API
        public static FieldInfo? FindPrivateField(this Type type, string privateFieldName)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).SingleOrDefault(f => f.Name == privateFieldName);
        }

        public static FieldInfo GetPrivateField(this Type type, string privateFieldName)
        {
            var privateField = type.FindPrivateField(privateFieldName);

            if (privateField != null)
            {
                return privateField;
            }

            var exception = new ArgumentOutOfRangeException(nameof(privateFieldName), $"Cannot find {privateFieldName} in {type}.");

            exception.Data.Add("type", type);
            exception.Data.Add("privateFieldName", privateFieldName);

            throw exception;
        }
    }
}