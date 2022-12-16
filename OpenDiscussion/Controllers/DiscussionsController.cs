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
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult Edit(int id)
        {
            Discussion discussion = db.Discussions.Find(id);
            return View(discussion);
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult Edit(int id, Discussion requestDiscussion)
        {
            Discussion discussion = db.Discussions.Find(id);
            requestDiscussion.DiscussionId = id;
            if (ModelState.IsValid)
            {
                if (_userManager.GetUserId(User) == discussion.UserId || User.IsInRole("Moderator") || User.IsInRole("Admin"))
                {
                    discussion.Title = requestDiscussion.Title;
                    discussion.Content = requestDiscussion.Content;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = discussion.TopicId });
                }
                else
                {
                    TempData["MessageDeny"] = "Nu aveti voie sa editati o discutie care nu va apartine";
                    return RedirectToAction("Index", new { id = discussion.TopicId }); 
                }
            }
            else
                return View(requestDiscussion);
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult New(int id)
        {
            Discussion discussion = new Discussion();
            discussion.TopicId = id;
            return View(discussion);
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult New(int id, Discussion discussion)
        {
            discussion.UserId = _userManager.GetUserId(User);
            discussion.Date = DateTime.Now;
            discussion.TopicId = id;
            if (ModelState.IsValid)
            {
                db.Discussions.Add(discussion);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = discussion.TopicId });
            }
            else
            {
                return View(discussion);
            }
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Discussion discussion = db.Discussions.Include("Comments").Where(diss => diss.DiscussionId == id).First();
            if (_userManager.GetUserId(User) == discussion.UserId || User.IsInRole("Moderator") || User.IsInRole("Admin"))
            {
                db.Discussions.Remove(discussion);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = discussion.TopicId });
            }
            else
            {
                TempData["MessageDeny"] = "Nu aveti voie sa stergeti o discutie care nu va apartine";
                return RedirectToAction("Index", new { id = discussion.TopicId });
            }

        }
        [Authorize(Roles = "User,Moderator,Admin")]
        public IActionResult Show(int id)
        {
            Discussion discussion = db.Discussions
                                    .Include("Comments")
                                    .Include("User")
                                    .Include("Comments.User")
                                    .Where(disc => disc.DiscussionId == id)
                                    .First();
            return View(discussion);
        }
        [Authorize(Roles = "User,Moderator,Admin")]
        [HttpPost]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.UserId = _userManager.GetUserId(User);
            comment.Date = DateTime.Now;
            if (ModelState.IsValid)
            { 
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Show", new { id = comment.DiscussionId });
            }
            else
            {
                Discussion discussion = db.Discussions.Include("Comments")
                                                      .Include("User")
                                                      .Include("Comments.User")
                                                      .Where(disc => disc.DiscussionId == comment.DiscussionId)
                                                      .First();
                return View(discussion);
            }
        }
    }
}
