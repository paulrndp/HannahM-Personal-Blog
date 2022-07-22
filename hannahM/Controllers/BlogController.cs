using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;

namespace hannahM.Controllers
{
    //[SessionExpire]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;


        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Published()
        {
            var status = _db.Blog.Where(x => x.BStatus == "published").Select(stats => stats).ToList();
            return View("Published", status);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Posts()
        {
            var status = _db.Blog.Where(x => x.BStatus == "draft").Select(stats => stats).ToList();
            return View("Posts", status);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var blogFound = _db.Blog.Find(id);
            if (blogFound == null)
            {
                return NotFound();
            }
            return View(blogFound);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var blogFound = _db.Blog.Find(id);
            if (blogFound == null)
            {
                return NotFound();
            }
            return View(blogFound);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog obj, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    Blog x = new Blog();
                    obj.BTitle = obj.BTitle;
                    obj.BContent = obj.BContent;
                    obj.BStatus = "draft";
                    _db.Blog.Update(obj);

                    _db.SaveChanges();
                }
                return RedirectToAction("Draft");

            }
            else if (submit == "Published")
            {
                if (ModelState.IsValid)
                {
                    Blog x = new Blog();
                    obj.BTitle = obj.BTitle;
                    obj.BContent = obj.BContent;
                    obj.BStatus = "published";
                    _db.Blog.Update(obj);
                    _db.SaveChanges();
                }
                return RedirectToAction("Published");

            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog postData, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    Blog obj = new Blog();
                    obj.BTitle = postData.BTitle;
                    obj.BContent = postData.BContent;
                    obj.BStatus = "draft";
                    _db.Blog.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Blog Draft!";
                    return RedirectToAction("Draft");
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
                    obj.BTitle = postData.BTitle;
                    obj.BContent = postData.BContent;
                    obj.BStatus = "published";
                    _db.Blog.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Blog Published!";
                    return RedirectToAction("Published");
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
        public IActionResult DeleteBlog(int? id, string submit)
        {

            var blogFound = _db.Blog.Find(id);
            if (blogFound == null)
            {
                return NotFound();
            }

            _db.Blog.Remove(blogFound);
            _db.SaveChanges();
            TempData["success"] = "Blog deleted!";

            if (submit == "draft")
            {
                return RedirectToAction("Draft");
            }
            else if (submit == "published")
            {
                return RedirectToAction("Published");
            }

            return View();


        }

    }
}
