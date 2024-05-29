namespace OpenMagic.Tests.TestHelpers
{
    public static class StringExtensions
    {
        public static string ArgumentExceptionMessage(this string argumentName, string message)
        {
            return $"{message} (Parameter '{argumentName}')";
        }
    }
}