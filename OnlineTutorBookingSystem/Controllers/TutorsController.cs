using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTutorBookingSystem.Data;
using System.Threading.Tasks;

namespace OnlineTutorBookingSystem.Controllers
{
    public class TutorsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TutorsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string q)
        {
            var tutors = _db.TutorProfiles.Include(t => t.User).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                tutors = tutors.Where(t => t.Headline.Contains(q) || t.Bio.Contains(q) || t.Languages.Contains(q));
            }

            var list = await tutors.OrderByDescending(t => t.IsSuperTutor).ThenByDescending(t => t.Rating).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var tutor = await _db.TutorProfiles.Include(t => t.User).FirstOrDefaultAsync(t => t.TutorProfileId == id);
            if (tutor == null) return NotFound();
            return View(tutor);
        }
    }
}
