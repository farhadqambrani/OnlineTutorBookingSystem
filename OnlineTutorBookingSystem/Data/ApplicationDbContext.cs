using Microsoft.EntityFrameworkCore;
using OnlineTutorBookingSystem.Models;

namespace OnlineTutorBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TutorProfile> TutorProfiles { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Student)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TutorProfile)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TutorProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TutorProfile>()
                .HasOne(t => t.User)
                .WithOne(u => u.TutorProfile)
                .HasForeignKey<TutorProfile>(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, SubjectName = "English" },
                new Subject { SubjectId = 2, SubjectName = "Mathematics" },
                new Subject { SubjectId = 3, SubjectName = "Computer Science" },
                new Subject { SubjectId = 4, SubjectName = "Physics" },
                new Subject { SubjectId = 5, SubjectName = "Chemistry" }
            );

            // Removed hard-coded TutorProfile seed data to ensure tutor listings show only real database records.
            // Seed subjects remain above; tutor profiles should be created by users through the application.
        }
    }
}