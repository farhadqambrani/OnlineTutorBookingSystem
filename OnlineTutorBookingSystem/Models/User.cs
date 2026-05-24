using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorBookingSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public TutorProfile? TutorProfile { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}