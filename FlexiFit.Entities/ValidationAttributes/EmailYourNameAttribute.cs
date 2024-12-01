using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiFit.Entities.ValidationAttributes
{
    public class EmailYourNameAttribute : ValidationAttribute
    {
        private readonly string _domainName;

        public EmailYourNameAttribute(string domainName)
        {
            _domainName = domainName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string email && email.EndsWith("@" + _domainName))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Email domain must be '{_domainName}'.");
        }
    }
}
