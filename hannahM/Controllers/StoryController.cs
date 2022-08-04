using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace hannahM.Controllers
{
    [SessionExpire]
    public class StoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Edit(int? id)
        {
            var status = _db.Stories.Where(x => x.Id == id).Select(stats => stats).Single();
            return View("Edit", status);
        }
        public IActionResult EditChapter(int? id)
        {
            var storyID = _db.Chapter.Where(x => x.Id == id).Select(stats => stats.story_id).Single();
            ViewBag.Title = _db.Stories.Where(x => x.Id == Convert.ToInt32(storyID)).Select(stats => stats.Title).Single();

            var status = _db.Chapter.Where(x => x.Id == id).Select(stats => stats).Single();
            return View("EditChapter", status);
        }
        public IActionResult NewChapter(int? id)
        {
            ViewBag.Title = _db.Stories.Where(x => x.Id == id).Select(stats => stats.Title).Single();
            return View();
        }
        public IActionResult Chapters(int? id)
        {
            var query = from s in _db.Stories
                        join c in _db.Chapter on s.Id equals c.story_id into x
                        from c in x.DefaultIfEmpty()
                        where s.Id == id
                        select new StoryViewModel
                        {
                            stories = s,
                            chapters = c
                        };
            return View(query);

        }
        public IActionResult Details(int id)
        {
            var status = _db.Stories.Where(x => x.Id == id).Select(stats => stats).Single();
            return View("Details", status);
        }
        public IActionResult MyStory()
        {
            var myStory = _db.Stories.Select(all => all).ToList();
            return View("MyStory", myStory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Story obj, List<IFormFile> Cover)
        {
            if (Cover.Count == 0)
            {
                if (ModelState.IsValid)
                {
                    
                    _db.Entry(obj).State = EntityState.Modified;
                    _db.Entry(obj).Property("Title").IsModified = true;
                    _db.Entry(obj).Property("Desc").IsModified = true;
                    _db.Entry(obj).Property("Tags").IsModified = true;
                    _db.Entry(obj).Property("Genre").IsModified = true;
                    _db.Entry(obj).Property("Cover").IsModified = false;
                    _db.SaveChanges();
                    TempData["success"] = "Successfully Updated! ";
                    return RedirectToAction("Edit");

                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";

                }
                return RedirectToAction("Edit");

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(obj).State = EntityState.Modified;
                    _db.Entry(obj).Property("Title").IsModified = true;
                    _db.Entry(obj).Property("Desc").IsModified = true;
                    _db.Entry(obj).Property("Tags").IsModified = true;
                    _db.Entry(obj).Property("Genre").IsModified = true;
                    foreach (var item in Cover)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                obj.Cover = stream.ToArray();
                            }
                        }
                    }
                    _db.SaveChanges();
                    TempData["success"] = "Successfully Updated! ";
                    return RedirectToAction("Edit");

                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";
                }
                return RedirectToAction("Edit");

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewChapter(Chapters obj)
        {
            if (ModelState.IsValid)
            {
                Chapters chptrs = new Chapters();
                chptrs.Title = obj.Title;
                chptrs.Content = obj.Content;
                chptrs.story_id = obj.story_id;
                _db.Chapter.Add(chptrs);
                _db.SaveChanges();
                TempData["success"] = "New Chapter Added.";
                return RedirectToAction("Chapters", new { id = obj.Id });
            }
            else
            {
                TempData["error"] = "There was an error submitting this form.";
                return RedirectToAction("NewChapter", obj);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditChapter(Chapters obj)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(obj).State = EntityState.Modified;
                _db.Entry(obj).Property("Title").IsModified = true;
                _db.Entry(obj).Property("Content").IsModified = true;
                _db.Entry(obj).Property("story_id").IsModified = false;
                _db.Entry(obj).Property("CreatedDateTime").IsModified = false; ;
                _db.SaveChanges();
                TempData["success"] = "'" + obj.Title + "'" + " Successfully Updated! ";
                return RedirectToAction("EditChapter");
            }
            return View(obj);
        }
        public IActionResult chapterDelete(int? id)
        {
            var chapter = _db.Chapter.Find(id);
            if (chapter == null)
            {
                return NotFound();
            }
            _db.Chapter.Remove(chapter);
            _db.SaveChanges();
            TempData["success"] = "Chapter successfully deleted.";
            return RedirectToAction("Chapters");
        }

        public IActionResult storyDelete(int? id)
        {
            _db.RemoveRange(_db.Stories.Where(c => c.Id == id));
            _db.RemoveRange(_db.Chapter.Where(c => c.story_id == id));
            _db.SaveChanges();

            TempData["success"] = "Story successfully deleted.";
            return RedirectToAction("MyStory");
        }
    }
}
