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
        public IActionResult StoryPost()
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
