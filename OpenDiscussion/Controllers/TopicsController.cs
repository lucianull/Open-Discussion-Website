﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Data;
using OpenDiscussion.Models;

namespace OpenDiscussion.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext db;
        public TopicsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index(int id)
        {
            ViewBag.TopicCategoryId = id;
            var topic = db.Topics.Where(top => top.CategoryId == id);
            ViewBag.Topics = topic;
            return View();
        }
        public IActionResult New(int id)
        {
            ViewBag.TopicCategoryId = id;
            return View();
        }
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
        public IActionResult Edit(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }
        [HttpPost]
        public IActionResult Edit(int id, Topic requestTopic)
        {
            if(ModelState.IsValid)
            {
                Topic topic = db.Topics.Find(id);
                topic.Name = requestTopic.Name;
                topic.Description = requestTopic.Description;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = topic.CategoryId });
            }
            else
                return View(requestTopic);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = topic.CategoryId });
        }

    }
}
