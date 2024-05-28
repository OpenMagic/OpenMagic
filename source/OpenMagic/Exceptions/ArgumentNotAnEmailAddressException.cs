using System;

namespace OpenMagic.Exceptions;

public class ArgumentNotAnEmailAddressException(string paramName, string value) : 
    ArgumentException("Value is not a valid email address.", paramName)
{
    public string Value { get; private set; } = value;
}