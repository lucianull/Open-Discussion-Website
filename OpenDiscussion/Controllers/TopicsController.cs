using Microsoft.AspNetCore.Mvc;

namespace OpenDiscussion.Controllers
{
    public class TopicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
