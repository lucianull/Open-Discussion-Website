using Microsoft.AspNetCore.Mvc;

namespace OpenDiscussion.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
