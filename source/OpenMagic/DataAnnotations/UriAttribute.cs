using System;
using System.ComponentModel.DataAnnotations;

namespace OpenMagic.DataAnnotations
{
    /// <summary>
    ///     Validates a string value is a Uri value.
    /// </summary>
    public class UriAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value.ToString())))
            {
                return ValidationResult.Success;
            }

            try
            {
                var stringValue = value.ToString();

                // Validate the string is a valid Uri
                _ = new Uri(stringValue);

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Value is not a valid Uri.");
            }
        }
    }
}