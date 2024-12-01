// FlexiFit.Entities/Class.cs
using FlexiFit.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexiFit.Entities
{
    /// <summary>
    /// Author: Alfred, Gurkaranjit, Kamaldeep
    /// Represents a gym class.
    /// </summary>
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
