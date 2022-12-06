using Microsoft.AspNetCore.Mvc;

namespace OpenDiscussion.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
