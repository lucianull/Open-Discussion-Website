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

        public IActionResult GetId(string id)
        {
            int profileId = db.Users.Include("Profile").Where(u => u.Id == id).First().Profile.ProfileId;
            return RedirectToAction("Show", new {id = profileId});

        }
        public IActionResult Show(int id) //id va fi id-ul profilului
        {
            Profile prof = db.Profiles.Find(id); //profilul userului
            var currentUser = db.Users.Include("Profile").Where(u => u.Id == prof.ApplicationUserId).First();
            return View(currentUser);
        }

        [HttpPost]
        public IActionResult Show(int id, [FromForm] ApplicationUser requestUser) //id va fi id-ul profilului
        {
            Profile prof = db.Profiles.Find(id); //profilul userului
            var currentUser = db.Users.Include("Profile").Where(u => u.Id == prof.ApplicationUserId).First();
            if (ModelState.IsValid)
            {
                prof.Description = requestUser.Profile.Description;
                prof.Avatar = requestUser.Profile.Avatar;
                db.SaveChanges();
            }
            return View(currentUser);
        }
    }
}
