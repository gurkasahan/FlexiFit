// FlexiFit.Entities/ValidationAttributes/EmailYourNameAttribute.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities.ValidationAttributes
{
    /// <summary>
    /// Custom validation attribute to ensure the email ends with a specific domain.
    /// </summary>
    public class EmailYourNameAttribute : ValidationAttribute
    {
        private readonly string _domainName;

        public EmailYourNameAttribute(string domainName)
        {
            _domainName = domainName;
        }

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
