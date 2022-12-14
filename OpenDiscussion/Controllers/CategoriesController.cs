using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult Index()
        {
            // var categories = db.Categories.Include("categories");
            var categories = from category in db.Categories select category;
            ViewBag.Categories = categories;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(int id, Category requestCategory)
        {
            Category category = db.Categories.Find(id);
            requestCategory.CategoryId = id;
            if (ModelState.IsValid)
            {
                category.Name = requestCategory.Name;
                category.Description = requestCategory.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(requestCategory);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost] 
        public IActionResult New(Category requestCategory)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(requestCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(requestCategory);
        }
        [Authorize(Roles = "Admin")]
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
