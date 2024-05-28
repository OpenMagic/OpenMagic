using System;

namespace OpenMagic.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when an argument value is empty.
    /// </summary>
    public class ArgumentEmptyException : ArgumentException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentEmptyException" /> class with the specified parameter name.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        public ArgumentEmptyException(string paramName)
            : base("Value cannot be empty.", paramName.MustNotBeNullOrWhiteSpace(nameof(paramName)))
        {
        }
    }
}