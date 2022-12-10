using Microsoft.AspNetCore.Mvc;
using OpenDiscussion.Data;
using OpenDiscussion.Models;

namespace OpenDiscussion.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly ApplicationDbContext db;
        public DiscussionsController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index(int id)
        {
            var discussions = db.Discussions.Where(disc => disc.TopicId == id);
            ViewBag.DiscussionTopicId = id;
            ViewBag.Discussions = discussions;
            return View();
        }
        public IActionResult Edit(int id)
        {
            Discussion discussion = db.Discussions.Find(id);
            return View(discussion);
        }
        [HttpPost]
        public IActionResult Edit(int id, Discussion requestDiscussion)
        {
            Discussion discussion = db.Discussions.Find(id);
            discussion.Title = requestDiscussion.Title;
            discussion.Content = requestDiscussion.Content;
            db.SaveChanges();
            return RedirectToAction("Index", new {id = discussion.TopicId});
        }
        public IActionResult New(int id)
        {
            ViewBag.DiscussionTopicId = id;
            return View();
        }
        [HttpPost]
        public IActionResult New(Discussion discussion)
        {
            db.Discussions.Add(discussion);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = discussion.TopicId});
        }
        public IActionResult Delete(int id)
        {
            Discussion discussion = db.Discussions.Find(id);
            db.Discussions.Remove(discussion);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = discussion.TopicId});
        }
    }
}
