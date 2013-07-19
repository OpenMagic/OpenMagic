using System;
using System.Text;

namespace OpenMagic
{
    /// <summary>
    /// Collection of methods to get a random string value.
    /// </summary>
    public class RandomString
    {
        /// <summary>
        /// Get a random string between 1 and 25 characters long.
        /// </summary>
        public static string Next()
        {
            // todo: unit tests.

            return Next(CharacterSets.Keyboard);
        }

        /// <summary>
        /// Get a random string between 1 and 25 characters long using <paramref name="characterSet"/> as the list of possible characters.
        /// </summary>
        public static string Next(string characterSet)
        {
            // todo: unit tests.

            return Next(1, 25, characterSet);
        }

        /// <summary>
        /// Get a random string between <paramref name="minLength"/> and <paramref name="maxLength"/> characters long.
        /// </summary>
        public static string Next(int minLength, int maxLength)
        {
            // todo: unit tests.

            return Next(minLength, maxLength, CharacterSets.Keyboard);
        }

        /// <summary>
        /// Get a random string between <paramref name="minLength"/> and <paramref name="maxLength"/> characters long.
        /// </summary>
        public static string Next(int minLength, int maxLength, string characterSet)
        {
            // todo: unit tests.

            minLength.MustBeGreaterThan(0, "minLength");
            maxLength.MustBeGreaterThan(minLength, "maxLength");
            characterSet.MustNotBeNullOrWhiteSpace("characterSet");

            var length = RandomNumber.NextInt(minLength, maxLength + 1);
            var value = new StringBuilder(length);
            var characters = characterSet.Length;

            for (var character = 1; character <= length; character++)
            {
                value.Append(characterSet.Substring(RandomNumber.NextInt(0, characters), 1));
            }

            return value.ToString();

        }

        /// <summary>
        /// Get a random string between 1 and 25 characters long with <paramref name="arguments"/> format items.
        /// </summary>
        /// <param name="formatItems">The number of format items to include at the end of the return string.</param>
        public static string NextFormat(int formatItems)
        {
            // todo: unit tests.

            if (formatItems < 1)
            {
                throw new ArgumentOutOfRangeException("formatItems", formatItems, "Number of format items must be 1 or more.");
            }

            var format = new StringBuilder(RandomString.Next(CharacterSets.AtoZ));

            for (var argument = 1; argument <= formatItems; argument++)
            {
                format.Append(" {" + (argument - 1).ToString() + "}");
            }

            return format.ToString();
        }
    }
}