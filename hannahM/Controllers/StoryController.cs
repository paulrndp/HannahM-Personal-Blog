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
        //VIEW
        public IActionResult Edit(int? id)
        {
            var status = _db.Stories.Where(x => x.Id == id).Select(stats => stats).Single();
            return View("Edit", status);

        }
        public IActionResult NewChapter(int? id)
        {
            //var query = _db.Chapter.Where(x => x.story_id == id).Select(all => all).ToList();
            //return View("NewChapter", query);

            //if (id == null || id == 0)
            //{
            //    return NotFound();
            //}
            var find = _db.Chapter.Find(id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }
        public IActionResult Chapters(int id)
        {

            var storyID = _db.Chapter.Where(x => x.Id == id).Select(stats => stats.story_id).Single();
            ViewBag.Title = _db.Stories.Where(x => x.Id == Convert.ToInt32(storyID)).Select(stats => stats.Title).Single();

            var query = _db.Chapter.Where(x => x.story_id == id).Select(all => all).ToList();
            return View("Chapters", query);

            //if (id == null || id == 0)
            //{
            //    return NotFound();
            //}
            //var dbFound = _db.Stories.Find(id);
            //if (dbFound == null)
            //{
            //    return NotFound();
            //}
            //return View(dbFound);
        }        
        public IActionResult Details(int id)
        {

            //var query = from s in _db.Stories
            //            join c in _db.Chapter on s.Id equals c.story_id into x
            //            from c in x.DefaultIfEmpty()
            //            where s.Id == id
            //            select new IndexVM { stories = s, chapters = c };
            //return View(query);


            var status = _db.Stories.Where(x => x.Id == id).Select(stats => stats).Single();
            return View("Details", status);

            //if (id == null || id == 0)
            //{
            //    return NotFound();
            //}
            //var dbFound = _db.Stories.Find(id);
            //if (dbFound == null)
            //{
            //    return NotFound();
            //}
            //return View(dbFound);

        }
        public IActionResult MyStory()
        {
            var myStory = _db.Stories.Select(all => all).ToList();
            return View("MyStory", myStory);

            //IEnumerable<StoryViewModel> mystories = new StoryViewModel();
            //return View(mystories);

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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Chapters obj, int id, string sID)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Entry(obj).State = EntityState.Modified;
        //        _db.Entry(obj).Property("Title").IsModified = true;
        //        _db.Entry(obj).Property("Content").IsModified = true;
        //        _db.Entry(obj).Property("story_id").IsModified = false;
        //        _db.Entry(obj).Property("CreatedDateTime").IsModified = false; ;
        //        _db.SaveChanges();
        //        TempData["success"] = obj.Title + " Successfully Updated! ";
        //        return RedirectToAction("Chapters", "Story", new { id = Convert.ToInt32(sID) });
        //    }
        //    return View(obj);
        //}
    }
}
