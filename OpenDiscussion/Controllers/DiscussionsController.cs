using Microsoft.AspNetCore.Mvc;

namespace OpenDiscussion.Controllers
{
    public class DiscussionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
