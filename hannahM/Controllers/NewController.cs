using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;

namespace hannahM.Controllers
{
    //[SessionExpire]
    public class NewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NewController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BlogPost()
        {
            return View();
        }
        public IActionResult RandomPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BlogPost(Blog postData, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    Blog obj = new Blog();
                    obj.Title = postData.Title;
                    obj.Content = postData.Content;
                    obj.Status = "draft";
                    _db.Blog.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Posts", "Blog");
                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";

                }
            }
            else if (submit == "Published")
            {
                if (ModelState.IsValid)
                {
                    Blog obj = new Blog();
                    obj.Title = postData.Title;
                    obj.Content = postData.Content;
                    obj.Status = "published";
                    _db.Blog.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as published post.";
                    return RedirectToAction("Posts", "Blog");
                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";
                }

            }
            return View(postData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandomPost(RandomThoughts postData, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    RandomThoughts obj = new RandomThoughts();
                    obj.Title = postData.Title;
                    obj.Content = postData.Content;
                    obj.Status = "draft";
                    _db.Random.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Posts", "Random");
                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";

                }
            }
            else if (submit == "Published")
            {
                if (ModelState.IsValid)
                {
                    RandomThoughts obj = new RandomThoughts();
                    obj.Title = postData.Title;
                    obj.Content = postData.Content;
                    obj.Status = "published";
                    _db.Random.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as published post.";
                    return RedirectToAction("Posts", "Random");
                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";
                }

            }
            return View(postData);
        }
    }
}
