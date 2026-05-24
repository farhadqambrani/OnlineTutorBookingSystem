using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTutorBookingSystem.Data;
using OnlineTutorBookingSystem.Models;
using System;
using System.Linq;

namespace OnlineTutorBookingSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsStudent()
        {
            return HttpContext.Session.GetString("Role") == "Student";
        }

        public IActionResult Dashboard()
        {
            if (!IsStudent())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View();
        }

        public IActionResult Tutors()
        {
            if (!IsStudent())
            {
                return RedirectToAction("Login", "Account");
            }

            // Load all tutor profiles including related User and Subject so newly created tutors appear immediately
            var tutors = _context.TutorProfiles
                .Include(t => t.User)
                .Include(t => t.Subject)
                .ToList();

            return View(tutors);
        }

        public IActionResult TutorDetails(int id)
        {
            if (!IsStudent())
            {
                return RedirectToAction("Login", "Account");
            }

            var tutor = _context.TutorProfiles
                .Include(t => t.User)
                .Include(t => t.Subject)
                .FirstOrDefault(t => t.TutorProfileId == id);

            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        public IActionResult BookLesson(int id)
        {
            if (!IsStudent())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TutorProfileId = id;
            return View();
        }

        [HttpPost]
        public IActionResult BookLesson(Booking booking)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || !IsStudent())
            {
                return RedirectToAction("Login", "Account");
            }

            bool duplicate = _context.Bookings.Any(b =>
                b.StudentId == userId &&
                b.TutorProfileId == booking.TutorProfileId &&
                b.BookingDate == booking.BookingDate &&
                b.BookingTime == booking.BookingTime);

            if (duplicate)
            {
                ViewBag.Error = "You already booked this tutor at the same date and time.";
                ViewBag.TutorProfileId = booking.TutorProfileId;
                return View(booking);
            }

            booking.StudentId = userId.Value;
            booking.Status = "Pending";
            booking.CreatedAt = DateTime.Now;

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("MyBookings");
        }

        public IActionResult MyBookings()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || !IsStudent())
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = _context.Bookings
                .Include(b => b.TutorProfile)
                .ThenInclude(t => t.User)
                .Include(b => b.TutorProfile)
                .ThenInclude(t => t.Subject)
                .Where(b => b.StudentId == userId)
                .ToList();

            return View(bookings);
        }
    }
}