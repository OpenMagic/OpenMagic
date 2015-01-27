using System;

namespace OpenMagic.Assertions
{
    public static class MustAssertions
    {
        public static void MustBe<T>(this T actualValue, T expectedValue, string exMessage)
        {
            (actualValue.Equals(expectedValue)).MustBeTrue(exMessage);
        }

        public static void MustBe<T>(this T actualValue, T expectedValue, string exMessage, params object[] exMessageArgs)
        {
            (actualValue.Equals(expectedValue)).MustBeTrue(exMessage, exMessageArgs);
        }

        public static void MustBeFalse(this bool assertion, string exMessage)
        {
            (assertion.Equals(false)).MustBeTrue(exMessage);
        }

        public static void MustBeFalse(this bool assertion, string exMessage, params object[] exMessageArgs)
        {
            (assertion.Equals(false)).MustBeTrue(exMessage, exMessageArgs);
        }
        
        public static void MustBeTrue(this bool assertion, string exMessage)
        {
            if (!assertion)
            {
                throw new AssertionException(exMessage);
            }
        }

        public static void MustBeTrue(this bool assertion, string exMessage, params object[] exMessageArgs)
        {
            assertion.MustBeTrue(String.Format(exMessage, exMessageArgs));
        }
    }
}
