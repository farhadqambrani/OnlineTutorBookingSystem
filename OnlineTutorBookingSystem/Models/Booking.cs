using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int StudentId { get; set; }

        public User? Student { get; set; }

        public int TutorProfileId { get; set; }

        public TutorProfile? TutorProfile { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public string BookingTime { get; set; } = string.Empty;

        public string? Message { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}