using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hannahM.Controllers
{
    //[SessionExpire]
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
            var find = _db.Chapter.Find(id);
            if (find == null)
            {
                return NotFound();
            }
            return View(find);
        }
        public IActionResult Chapters(int? id)
        {

            var getId = _db.Chapter.Where(x => x.story_id == id).Select(stats => stats.story_id).SingleOrDefault();
            int storyId = Convert.ToInt32(getId);

            ViewBag.Title = _db.Stories.Where(x => x.Id == storyId).Select(stats => stats.Title).SingleOrDefault();
            ViewBag.Desc = _db.Stories.Where(x => x.Id == storyId).Select(stats => stats.Desc).SingleOrDefault();
            ViewBag.Cover = _db.Stories.Where(x => x.Id == storyId).Select(stats => stats.Cover).SingleOrDefault();

            var query = from s in _db.Stories
                        join c in _db.Chapter on s.Id equals c.story_id into x from c in x.DefaultIfEmpty()
                        where c.story_id == id
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
                    return RedirectToAction("Edit", obj);

                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";

                }
                return RedirectToAction("Edit", obj);

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
                    _db.Stories.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Successfully Updated! ";
                    return RedirectToAction("Edit", obj);

                }
                else
                {
                    TempData["error"] = "There was an error submitting this form.";
                }
                return RedirectToAction("Edit", obj);

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
                ;
                _db.Chapter.Add(chptrs);
                _db.SaveChanges();
                TempData["success"] = "Successfully Created ";
                return RedirectToAction("MyStory");
            }
            return View(obj);
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
                TempData["success"] = "'"+obj.Title+"'"+" Successfully Updated! ";
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
    }
}
