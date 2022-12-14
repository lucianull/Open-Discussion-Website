using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Data;
using OpenDiscussion.Models;
using System.Data;

namespace OpenDiscussion.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DiscussionsController(ApplicationDbContext context,
                                     UserManager<ApplicationUser> userManager,
                                     RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult Index(int id)
        {
            var discussions = db.Discussions.Include("User").Where(disc => disc.TopicId == id);
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
            if (ModelState.IsValid)
            {
                discussion.Title = requestDiscussion.Title;
                discussion.Content = requestDiscussion.Content;
                db.SaveChanges();
                return RedirectToAction("Index", new {id = discussion.TopicId});
            }
            else
                return View(requestDiscussion);
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult New(int id)
        {
            ViewBag.DiscussionTopicId = id;
            return View();
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult New(Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                discussion.Date = DateTime.Now;
                db.Discussions.Add(discussion);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = discussion.TopicId });
            }
            else
                return View(discussion);
        }
        public IActionResult Delete(int id)
        {
            Discussion discussion = db.Discussions.Find(id);
            db.Discussions.Remove(discussion);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = discussion.TopicId});
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult Show(int id)
        {
            Discussion discussion = db.Discussions.Include("Comments").Include("User").Where(disc => disc.DiscussionId == id).First();
            return View(discussion);
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult Show([FromForm] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Date = DateTime.Now;

                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Show", new { id = comment.DiscussionId });
            }
            else
            {
                Discussion discussion = db.Discussions.Include("Comments").Include("User").Where(disc => disc.DiscussionId == comment.DiscussionId).First();
                return View(discussion);
            }
        }
    }
}
