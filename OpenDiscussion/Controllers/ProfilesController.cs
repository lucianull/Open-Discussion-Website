using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Show(int id) //id va fi id-ul profilului
        {
            Profile prof = db.Profiles.Find(id); //profilul userului
            ApplicationUser currentUser = db.Users.Include("Profile").Where(u => u.Id == prof.ApplicationUserId).First();
            return View(currentUser);
        }
    }
}
