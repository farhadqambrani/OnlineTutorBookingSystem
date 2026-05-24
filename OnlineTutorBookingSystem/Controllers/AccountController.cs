using Microsoft.AspNetCore.Mvc;
using OnlineTutorBookingSystem.Data;
using OnlineTutorBookingSystem.Models;
using System.Linq;

namespace OnlineTutorBookingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool emailExists = _context.Users.Any(u => u.Email == user.Email);

                if (emailExists)
                {
                    ViewBag.Error = "Email already exists.";
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Tutor")
                {
                    return RedirectToAction("Dashboard", "Tutor");
                }

                return RedirectToAction("Dashboard", "Student");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId.Value);
            if (user == null) return RedirectToAction("Login");

            return View(user);
        }
    }
}