using System;

namespace OpenMagic.Exceptions
{
    public class ArgumentNotValidEmailAddress : ArgumentException
    {
        public ArgumentNotValidEmailAddress(string paramName, string value) : base("Value is not a valid email address.", paramName)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}