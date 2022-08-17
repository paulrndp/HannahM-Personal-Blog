using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;

namespace hannahM.Controllers
{
    [SessionExpire]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;


        public PostsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index(string show)
        {
            if (show == "All")
            {
                ViewBag.all = "active";
                var status = _db.Post.OrderByDescending(x => x.Id).ToList();
                return View("Index", status);
            }
            else if (show == "Blog")
            {
                ViewBag.blog = "active";
                var status = _db.Post.Where(x => x.Category == "Blog").Select(x => x).ToList();
                return View("Index", status);
            }
            else if (show == "Random")
            {
                ViewBag.random = "active";
                var status = _db.Post.Where(x => x.Category == "Random").Select(x => x).ToList();
                return View("Index", status);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var found = _db.Post.Find(id);
            if (found == null)
            {
                return NotFound();
            }
            return View(found);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Posts obj, string submit, string blog, string random)
        {
            if (blog == "Blog" && random == null)
            {
                if (submit == "Draft")
                {
                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    obj.Status = "draft";
                    obj.Category = "Blog";

                    _db.Post.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });
                }
                else if (submit == "Published")
                {
                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    obj.Status = "published";
                    obj.Category = "Blog";
                    _db.Post.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as published post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
                return View(obj);
            }
            else if (random == "Random" && blog == null)
            {
                if (submit == "Draft")
                {
                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    obj.Status = "draft";
                    obj.Category = "Random";
                    _db.Post.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
                else if (submit == "Published")
                {

                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    obj.Status = "published";
                    obj.Category = "Random";
                    _db.Post.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as published post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }

            }
            else if (blog == "Blog" && random == "Random")
            {
                if (submit == "Draft")
                {

                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    obj.Status = "draft";
                    obj.Category = "Both";
                    _db.Post.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
                else if (submit == "Published")
                {

                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    obj.Status = "published";
                    obj.Category = "Both";
                    _db.Post.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as published post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
            }

            return View(obj);

        }
        public IActionResult postDelete(int? id)
        {
            var found = _db.Post.Find(id);
            if (found == null)
            {
                return NotFound();
            }
            _db.Post.Remove(found);
            _db.SaveChanges();
            TempData["success"] = "Post successfully deleted.";
            return RedirectToAction("Index", new { show = "All" });
        }
    }
}
