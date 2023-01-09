using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenDiscussion.Data;
using OpenDiscussion.Models;
using System.Diagnostics;

namespace OpenDiscussion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        private readonly UserManager <ApplicationUser> _userManager;
        private readonly RoleManager < IdentityRole > _roleManager;
        public HomeController(ApplicationDbContext context,
            ILogger<HomeController> logger,
            UserManager <ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Categories");
            var Discussions = (from discussion in db.Discussions select discussion);
            Random random= new Random();
            int DiscussionIndex = random.Next(1, Discussions.Count());
            Discussion discutie = Discussions.Skip(DiscussionIndex).Take(1).First();
            ViewBag.DisplayArticle = discutie;
            return View();
        }

        public IActionResult Privacy()
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