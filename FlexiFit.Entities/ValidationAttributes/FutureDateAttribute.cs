using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiFit.Entities.ValidationAttributes 
{
    public class FutureDateAttribute : ValidationAttribute
    {
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
