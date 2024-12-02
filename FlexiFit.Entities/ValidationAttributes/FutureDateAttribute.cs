using System;
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities.ValidationAttributes
{
    /// <summary>
    /// Principal Author: [Your Name]
    /// Custom validation attribute to ensure a date is in the future.
    /// </summary>
    public class FutureDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates whether the given date is in the future.
        /// </summary>
        /// <param name="value">The date to validate.</param>
        /// <param name="validationContext">The context of the validation.</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date && date >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Booking date and time must be in the future.");
        }
    }
}
