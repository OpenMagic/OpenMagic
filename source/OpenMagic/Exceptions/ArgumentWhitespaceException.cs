﻿using System;

namespace OpenMagic.Exceptions
{
    /// <summary>
    ///     The exception that is thrown when an argument value is whitespace.
    /// </summary>
    public class ArgumentWhiteSpaceException : ArgumentException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentWhiteSpaceException" /> class with the specified parameter name.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        public ArgumentWhiteSpaceException(string paramName)
            : base("Value cannot be whitespace.", paramName)
        {
        }
    }
}