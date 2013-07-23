using System;
using System.ComponentModel.DataAnnotations;

namespace OpenMagic.DataAnnotations
{
    /// <summary>
    /// Validates a string value is a Uri value.
    /// </summary>
    public class UriAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((value == null) || (value is string && string.IsNullOrWhiteSpace(value.ToString())))
            {
                return true;
            }

            try
            {
                var uri = new Uri(value.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
