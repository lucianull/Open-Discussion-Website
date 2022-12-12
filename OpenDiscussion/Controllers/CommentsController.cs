using Microsoft.AspNetCore.Mvc;
using OpenDiscussion.Data;
using OpenDiscussion.Models;

namespace OpenDiscussion.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CommentsController(ApplicationDbContext context)
        {
            db= context;
        }
        public IActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }
        [HttpPost]
        public IActionResult Edit(int id, Comment requestComment)
        {
            if (ModelState.IsValid)
            {
                ViewBag.IdAux = id;
                Console.WriteLine("Pula mea " + id.ToString() + "\n");
                Comment comment = db.Comments.Find(id);
                comment.Content = requestComment.Content;
                db.SaveChanges();
                return Redirect("/Discussions/Show/" + comment.DiscussionId);
            }
            else
            {
                ViewBag.IdAux = id;
                return View(requestComment);
            }
        }

        public IActionResult Delete(int id) 
        { 
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return Redirect("/Discussions/Show/" + comment.DiscussionId);
        }
    }
}
