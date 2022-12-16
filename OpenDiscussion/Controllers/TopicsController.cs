using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Data;
using OpenDiscussion.Models;
using System.Data;

namespace OpenDiscussion.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext db;
        public TopicsController(ApplicationDbContext context)
        {
            db = context;
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult Index(int id)
        {
            ViewBag.TopicCategoryId = id;
            var topic = db.Topics.Where(top => top.CategoryId == id);
            ViewBag.Topics = topic;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult New(int id)
        {
            ViewBag.TopicCategoryId = id;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult New(Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = topic.CategoryId });
            }
            else
                return View(topic);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(int id, Topic requestTopic)
        {
            Topic topic = db.Topics.Find(id);
            requestTopic.TopicId = id;
            if (ModelState.IsValid)
            {
                topic.Name = requestTopic.Name;
                topic.Description = requestTopic.Description;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = topic.CategoryId });
            }
            else
                return View(requestTopic);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Topic topic = db.Topics.Include("Discussions").Where(top => top.TopicId == id).First();
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = topic.CategoryId });
        }

    }
}
