// FlexiFit.Entities/Member.cs
//using FlexiFit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities.Models
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Represents a gym member.
    /// </summary>
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "First name must not include any digits.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Last name must not include any digits.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [EmailYourName("gmail.com")] // Custom validation attribute
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public MembershipTier MembershipTier { get; set; } // Enum: Regular, Premium

        [Required]
        public string Address { get; set; }

        [Required]
        public string BillingName { get; set; }

        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Booking> Bookings { get; set; }
    }

    public enum MembershipTier
    {
        Regular,
        Premium
    }
}
