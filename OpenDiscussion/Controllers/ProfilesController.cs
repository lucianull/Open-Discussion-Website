using Microsoft.AspNetCore.Mvc;
using OpenDiscussion.Data;
using OpenDiscussion.Models;

namespace OpenDiscussion.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProfilesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Show(int id)
        {
            ApplicationUser currentUser = db.ApplicationUser.Where(prof => prof.UserId == id).First();
            return View(profile);
        }
    }
}
