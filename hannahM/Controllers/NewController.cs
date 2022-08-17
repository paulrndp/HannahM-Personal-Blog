using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hannahM.Controllers
{
    [SessionExpire]
    public class NewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NewController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BlogPost()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RandomPost()
        {
            return View();
        }
        [HttpGet]
        public IActionResult StoryPost()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(Posts obj, string submit, string blog, string random)
        {
            if (blog == "Blog" && random == null)
            {
                if (submit == "Draft")
                {
                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    p.Status = "draft";
                    p.Category = "Blog";
                    _db.Post.Add(p);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Index", "Posts", new { show = "All"});
                }
                else if (submit == "Published")
                {
                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    p.Status = "published";
                    p.Category = "Blog";
                    _db.Post.Add(p);
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
                    p.Status = "draft";
                    p.Category = "Random";
                    _db.Post.Add(p);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
                else if (submit == "Published")
                {

                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    p.Status = "published";
                    p.Category = "Random";
                    _db.Post.Add(p);
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
                    p.Status = "draft";
                    p.Category = "Both";
                    _db.Post.Add(p);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as draft post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
                else if (submit == "Published")
                {

                    Posts p = new Posts();
                    p.Title = obj.Title;
                    p.Content = obj.Content;
                    p.Status = "published";
                    p.Category = "Both";
                    _db.Post.Add(p);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully created as published post.";
                    return RedirectToAction("Index", "Posts", new { show = "All" });

                }
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoryPost(Story s, List<IFormFile> Cover)
        {
            if (ModelState.IsValid)
            {
                Story story = new Story();
                story.Title = s.Title;
                story.Desc = s.Desc;
                story.Tags = s.Tags;
                story.Genre = s.Genre;

                foreach (var item in Cover)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            story.Cover = stream.ToArray();
                        }
                    }
                }
                _db.Stories.Add(story);
                _db.SaveChanges();
                TempData["success"] = "New story added.";

            }
            else
            {
                TempData["error"] = "There was an error submitting this form.";
                return RedirectToAction("StoryPost", s);
            }
            return RedirectToAction("MyStory", "Story");

        }

    }
}
