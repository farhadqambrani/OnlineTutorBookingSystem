using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTutorBookingSystem.Data;
using OnlineTutorBookingSystem.Models;
using System.Linq;

namespace OnlineTutorBookingSystem.Controllers
{
    public class TutorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutorController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsTutor()
        {
            return HttpContext.Session.GetString("Role") == "Tutor";
        }

        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || !IsTutor())
            {
                return RedirectToAction("Login", "Account");
            }

            // load tutor profile for the currently logged in tutor
            var profile = _context.TutorProfiles
                .Include(t => t.Subject)
                .FirstOrDefault(t => t.UserId == userId.Value);

            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            // if no profile exists yet, redirect to create page
            if (profile == null)
            {
                return RedirectToAction("CreateProfile");
            }

            return View(profile);
        }

        public IActionResult CreateProfile()
        {
            if (!IsTutor())
            {
                return RedirectToAction("Login", "Account");
            }

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // if profile already exists, send tutor to dashboard
            var exists = _context.TutorProfiles.Any(t => t.UserId == userId.Value);
            if (exists)
            {
                return RedirectToAction("Dashboard");
            }

            ViewBag.Subjects = _context.Subjects.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile(TutorProfile profile)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || !IsTutor())
            {
                return RedirectToAction("Login", "Account");
            }

            // double-check duplicates using current user's id
            if (_context.TutorProfiles.Any(t => t.UserId == userId.Value))
            {
                // already has a profile - show friendly message instead of failing silently
                ModelState.AddModelError(string.Empty, "You already have a tutor profile.");
                ViewBag.Subjects = _context.Subjects.ToList();
                return View(profile);
            }

            // ensure a subject was selected
            if (profile.SubjectId <= 0 || !_context.Subjects.Any(s => s.SubjectId == profile.SubjectId))
            {
                ModelState.AddModelError("SubjectId", "Please select a valid subject.");
            }

            if (ModelState.IsValid)
            {
                profile.UserId = userId.Value;
                profile.CreatedAt = System.DateTime.Now;

                _context.TutorProfiles.Add(profile);
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
                catch (DbUpdateException)
                {
                    // Provide validation message if persistence fails
                    ModelState.AddModelError(string.Empty, "Unable to save tutor profile. Please try again later.");
                }
            }

            ViewBag.Subjects = _context.Subjects.ToList();
            return View(profile);
        }

        public IActionResult EditProfile(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            var profile = _context.TutorProfiles
                .FirstOrDefault(t => t.TutorProfileId == id && t.UserId == userId);

            if (profile == null)
            {
                return NotFound();
            }

            ViewBag.Subjects = _context.Subjects.ToList();
            return View(profile);
        }

        [HttpPost]
        public IActionResult EditProfile(TutorProfile profile)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            var existingProfile = _context.TutorProfiles
                .FirstOrDefault(t => t.TutorProfileId == profile.TutorProfileId && t.UserId == userId);

            if (existingProfile == null)
            {
                return NotFound();
            }

            existingProfile.SubjectId = profile.SubjectId;
            existingProfile.HourlyRate = profile.HourlyRate;
            existingProfile.Experience = profile.Experience;
            existingProfile.Bio = profile.Bio;
            existingProfile.Availability = profile.Availability;

            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        public IActionResult BookingRequests()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || !IsTutor())
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = _context.Bookings
                .Include(b => b.Student)
                .Include(b => b.TutorProfile)
                .ThenInclude(t => t.Subject)
                .Where(b => b.TutorProfile.UserId == userId)
                .ToList();

            return View(bookings);
        }

        public IActionResult AcceptBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);

            if (booking != null)
            {
                booking.Status = "Accepted";
                _context.SaveChanges();
            }

            return RedirectToAction("BookingRequests");
        }

        public IActionResult RejectBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);

            if (booking != null)
            {
                booking.Status = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("BookingRequests");
        }
    }
}