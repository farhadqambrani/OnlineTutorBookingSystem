using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorBookingSystem.Models
{
    public class TutorProfile
    {
        [Key]
        public int TutorProfileId { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        // Short headline shown in the tutor list
        public string Headline { get; set; } = string.Empty;

        [Required]
        public string Experience { get; set; } = string.Empty;

        [Required]
        public string Bio { get; set; } = string.Empty;

        // Comma-separated languages (simple)
        public string Languages { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public bool IsSuperTutor { get; set; } = false;

        public int StudentsCount { get; set; } = 0;

        public int LessonsCount { get; set; } = 0;

        // URL to profile picture (can be external)
        public string? ProfilePictureUrl { get; set; }

        [Required]
        public string Availability { get; set; } = string.Empty;

        public decimal Rating { get; set; } = 0m;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Booking>? Bookings { get; set; }
    }
}