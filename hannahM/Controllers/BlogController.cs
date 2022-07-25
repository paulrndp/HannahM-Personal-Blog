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
        public IActionResult Posts()
        {

            var status = _db.Blog.OrderByDescending(x => x.Id).ToList();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog obj, string submit)
        {
            if (submit == "Draft")
            {
                if (ModelState.IsValid)
                {
                    Blog x = new Blog();
                    x.Title = obj.Title;
                    x.Content = obj.Content;
                    obj.Status = "draft";
                    _db.Blog.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully updated as draft post.";

                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";
                    return View("Edit", obj);

                }
                return View("Edit", obj);

            }
            else if (submit == "Published")
            {
                if (ModelState.IsValid)
                {
                    Blog x = new Blog();
                    x.Title = obj.Title;
                    x.Content = obj.Content;
                    obj.Status = "published";
                    _db.Blog.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully updated as published post.";

                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";
                    return View("Edit", obj);

                }
                return View("Edit", obj);

            }

            return View(obj);

        }

        public IActionResult postDelete(int? id)
        {
            var blog = _db.Blog.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            _db.Blog.Remove(blog);
            _db.SaveChanges();
            TempData["success"] = "Blog successfully deleted.";
            return RedirectToAction("Posts");
        }
    }
}
