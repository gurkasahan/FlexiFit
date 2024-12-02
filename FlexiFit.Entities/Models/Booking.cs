// FlexiFit.Entities/Booking.cs
//using FlexiFit.Entities.Members.cs;//
using System;
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities.Models
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Represents a booking of a class by a member.
    /// </summary>
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [Required]
        public int ClassId { get; set; }
        public Class Class { get; set; }

        [Required]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required]
        [Display(Name = "Booking Time")]
        public TimeSpan BookingTime { get; set; }
    }
}
