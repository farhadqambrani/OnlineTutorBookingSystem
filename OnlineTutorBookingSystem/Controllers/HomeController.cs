using Microsoft.AspNetCore.Mvc;
using OnlineTutorBookingSystem.Models;
using OnlineTutorBookingSystem.Data;
using System.Linq;
using System.Diagnostics;

namespace OnlineTutorBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Subjects page - lists available subjects
        public IActionResult Subjects()
        {
            var subjects = _context.Subjects
                .OrderBy(s => s.SubjectName)
                .ToList();

            return View(subjects);
        }

        // About page - information and mission
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
