using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorBookingSystem.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [Required]
        public string SubjectName { get; set; } = string.Empty;

        public ICollection<TutorProfile>? TutorProfiles { get; set; }
    }
}