using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Data;
using OpenDiscussion.Models;

namespace OpenDiscussion.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoriesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            // var categories = db.Categories.Include("categories");
            var categories = from category in db.Categories select category;
            ViewBag.Categories = categories;
            return View();
        }
        public IActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(int id, Category requestCategory)
        {
            Category category = db.Categories.Find(id);
            category.Name = requestCategory.Name;
            category.Description = requestCategory.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult New()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult New(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
