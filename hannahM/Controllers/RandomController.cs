﻿using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;

namespace hannahM.Controllers
{
    [SessionExpire]
    public class RandomController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RandomController(ApplicationDbContext db)
        {
            _db = db;
        }
        //VIEW
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult RPublished()
        {
            var status = _db.Random.Where(x => x.RStatus == "published").Select(stats => stats).ToList();
            return View("RPublished", status);
        }        
        public IActionResult RDraft()
        {
            var status = _db.Random.Where(x => x.RStatus == "draft").Select(stats => stats).ToList();
            return View("RDraft", status);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dbFound = _db.Random.Find(id);
            if (dbFound == null)
            {
                return NotFound();
            }
            return View(dbFound);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dbFound = _db.Random.Find(id);
            if (dbFound == null)
            {
                return NotFound();
            }
            return View(dbFound);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RandomThoughts postData, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    RandomThoughts obj = new RandomThoughts();
                    obj.RTitle = postData.RTitle;
                    obj.RContent = postData.RContent;
                    obj.RStatus = "draft";
                    _db.Random.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Random Thoughts Draft!";
                    return RedirectToAction("RDraft");
                }

            }
            else if (submit == "Published")
            {
                if (ModelState.IsValid)
                {
                    RandomThoughts obj = new RandomThoughts();
                    obj.RTitle = postData.RTitle;
                    obj.RContent = postData.RContent;
                    obj.RStatus = "published";
                    _db.Random.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Random Thoughts Published!";
                    return RedirectToAction("RPublished");
                }

            }
            return View(postData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RandomThoughts obj, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    Random x = new Random();
                    obj.RTitle = obj.RTitle;
                    obj.RContent = obj.RContent;
                    obj.RStatus = "draft";
                    _db.Random.Update(obj);

                    _db.SaveChanges();
                }
                return RedirectToAction("RDraft");
            }
            else if (submit == "Published")
            {
                if (ModelState.IsValid)
                {
                    Random x = new Random();
                    obj.RTitle = obj.RTitle;
                    obj.RContent = obj.RContent;
                    obj.RStatus = "published";
                    _db.Random.Update(obj);
                    _db.SaveChanges();
                }
                return RedirectToAction("RPublished");
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRandom(int? id, string submit)
        {

            var blogFound = _db.Random.Find(id);
            if (blogFound == null)
            {
                return NotFound();
            }

            _db.Random.Remove(blogFound);
            _db.SaveChanges();
            TempData["success"] = "Random Thoughts Deleted!";

            if (submit == "draft")
            {
                return RedirectToAction("RDraft");
            }
            else if (submit == "published")
            {
                return RedirectToAction("RPublished");
            }

            return View();


        }
    }
}