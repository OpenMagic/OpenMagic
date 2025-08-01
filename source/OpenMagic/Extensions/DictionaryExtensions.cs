﻿using System.Collections.Generic;

namespace OpenMagic.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Finds the value associated with the specified key.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="key">The key of the value to find.</param>
        /// <returns>
        ///     The value associated with the specified key, if the key is found; otherwise, the default value for the type of
        ///     the value parameter.
        /// </returns>
        public static TValue? FindValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out var value);

            return value;
        }
    }
}