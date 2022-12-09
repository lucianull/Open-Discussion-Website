using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Data;
using OpenDiscussion.Models;

namespace OpenDiscussion.Controllers
{
    public class TopicsController : Controller
    {
        ApplicationDbContext db;
        private int CategoryId;
        public TopicsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index(int id)
        {
            CategoryId = id;
            var topic = db.Topics.Where(top => top.CategoryId == id);
            ViewBag.Topics = topic;
            return View();
        }
        public IActionResult New()
        {
            ViewBag.CategoryIdVbg = CategoryId;
            return View();
        }
        [HttpPost]
        public IActionResult New(Topic topic)
        {
            topic.CategoryId = CategoryId;
            db.Topics.Add(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }
        [HttpPost]
        public IActionResult Edit(int id, Topic requestTopic)
        {
            Topic topic = db.Topics.Find(id);
            topic.Name = requestTopic.Name;
            topic.Description = requestTopic.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
