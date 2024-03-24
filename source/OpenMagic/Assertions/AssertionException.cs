using System;

namespace OpenMagic.Assertions;

/// <summary>
///     The exception that is thrown when an assertion is false.
/// </summary>
public class AssertionException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AssertionException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public AssertionException(string message)
        : base(message)
    {
    }
}