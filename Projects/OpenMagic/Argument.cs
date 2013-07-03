using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenMagic
{
    public static class Argument
    {
        public static T MustNotBeNull<T>(T param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return param;
        }

        public static IEnumerable<T> MustNotBeNullOrEmpty<T>(IEnumerable<T> param, string paramName)
        {
            Argument.MustNotBeNull(param, paramName);

            if (!param.Any())
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }

            return param;
        }

        public static string MustNotBeNullOrWhiteSpace(string param, string paramName)
        {
            Argument.MustNotBeNull(param, paramName);

            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentException("Value cannot be whitespace.", paramName);
            }

            return param;
        }
    }
}
