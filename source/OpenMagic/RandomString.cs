using System;
using System.Text;

namespace OpenMagic
{
    /// <summary>
    ///     Collection of methods to get a random string value.
    /// </summary>
    public class RandomString
    {
        /// <summary>
        ///     Get a random string between 1 and 25 characters long.
        /// </summary>
        public static string Next()
        {
            return Next(CharacterSets.Keyboard);
        }

        /// <summary>
        ///     Get a random string between 1 and 25 characters long using <paramref name="characterSet" /> as the list of possible
        ///     characters.
        /// </summary>
        public static string Next(string characterSet)
        {
            return Next(1, 25, characterSet);
        }

        /// <summary>
        ///     Get a random string between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long.
        /// </summary>
        public static string Next(int minLength, int maxLength)
        {
            return Next(minLength, maxLength, CharacterSets.Keyboard);
        }

        /// <summary>
        ///     Get a random string between <paramref name="minLength" /> and <paramref name="maxLength" /> characters long.
        /// </summary>
        public static string Next(int minLength, int maxLength, string characterSet)
        {
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
        ///     Get a random string between 1 and 25 characters long with <paramref name="arguments" /> format items.
        /// </summary>
        /// <param name="formatItems">The number of format items to include at the end of the return string.</param>
        public static string NextFormat(int formatItems)
        {
            if (formatItems < 1)
            {
                var exception = new ArgumentOutOfRangeException("formatItems",
                    "Number of format items must be 1 or more.");

                exception.Data.Add("formatItems", formatItems);

                throw exception;
            }

            var format = new StringBuilder(Next(CharacterSets.AtoZ));

            for (var argument = 1; argument <= formatItems; argument++)
            {
                format.Append(" {" + (argument - 1) + "}");
            }

            return format.ToString();
        }

        /// <summary>
        ///     Get a random email address.
        /// </summary>
        public static string NextEmailAddress()
        {
            var user = Next(1, 30);
            var domain = Next(2, 30);
            var tld = Next(2, 6, CharacterSets.LowerAtoZ);

            // 1 in 9 email addresses have a country extension
            if (RandomNumber.NextInt(1, 10) == 3)
            {
                tld += "." + Next(2, 3, CharacterSets.LowerAtoZ);
            }

            return string.Format("{0}@{1}.{2}", user, domain, tld);
        }
    }
}