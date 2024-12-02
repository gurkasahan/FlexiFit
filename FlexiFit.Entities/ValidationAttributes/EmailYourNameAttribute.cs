using System;
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities.ValidationAttributes
{
    /// <summary>
    /// Principal Author: [Your Name]
    /// Custom validation attribute to validate email addresses with a specific domain.
    /// </summary>
    public class EmailYourNameAttribute : ValidationAttribute
    {
        private readonly string _domainName;

        /// <summary>
        /// Initializes the attribute with the required domain name.
        /// </summary>
        /// <param name="domainName">The domain name to validate against.</param>
        public EmailYourNameAttribute(string domainName)
        {
            _domainName = domainName;
        }

        /// <summary>
        /// Validates whether the given email ends with the specified domain.
        /// </summary>
        /// <param name="value">The email address to validate.</param>
        /// <param name="validationContext">The context of the validation.</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = value as string;
            if (!string.IsNullOrEmpty(email))
            {
                if (email.EndsWith("@" + _domainName, StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Email domain must be '{_domainName}'.");
                }
            }
            return new ValidationResult("Invalid email address.");
        }
    }
}
