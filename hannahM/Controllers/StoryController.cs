using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //VIEW
        public IActionResult SCreate()
        {

            return View();
        }
        public IActionResult Edit(int? id)
        {
            var find = _db.Chapter.Find(id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);

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

            //IEnumerable<Story> mystories = _db.Stories;
            //return View(mystories);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SCreate(IndexVM obj, List<IFormFile> Cover)
        {

            if (ModelState.IsValid)
            {
                Story story = new Story();
                story.Title = obj.stories.Title;
                story.Desc = obj.stories.Desc;
                story.Tags = obj.stories.Tags;
                story.Genre = obj.stories.Genre;

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

                Chapters chptrs = new Chapters();
                chptrs.Title = obj.chapters.Title;
                chptrs.Content = obj.chapters.Content;
                chptrs.story_id = story.Id;

                _db.Chapter.Add(chptrs);
                _db.SaveChanges();
                TempData["success"] = "Successfully Created " + story.Title;
                return RedirectToAction("MyStory");
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Story obj, List<IFormFile> Cover)
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
                    return RedirectToAction("Details");
                }
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
                    return RedirectToAction("Details");
                }

            }
            return View(obj);
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
        public IActionResult Edit(Chapters obj, int id, string sID)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(obj).State = EntityState.Modified;
                _db.Entry(obj).Property("Title").IsModified = true;
                _db.Entry(obj).Property("Content").IsModified = true;
                _db.Entry(obj).Property("story_id").IsModified = false;
                _db.Entry(obj).Property("CreatedDateTime").IsModified = false; ;
                _db.SaveChanges();
                TempData["success"] = obj.Title + " Successfully Updated! ";
                return RedirectToAction("Chapters", "Story", new { id = Convert.ToInt32(sID) });
            }
            return View(obj);
        }
    }
}
