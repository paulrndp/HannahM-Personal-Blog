using hannahM.Data;
using hannahM.Models;
using Microsoft.AspNetCore.Mvc;
using hannahM.Action;
using Microsoft.EntityFrameworkCore;

namespace hannahM.Controllers
{
    public class MainController : Controller
    {

        private readonly ApplicationDbContext _db;
        static string mainIP = "";

        public MainController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            using (var wc = new System.Net.WebClient())
                mainIP = wc.DownloadString("https://checkip.amazonaws.com/");


            ViewBag.Blog = "Blog";
            ViewBag.Stories = "Stories";
            ViewBag.RandomThoughts = "Random Thoughts";
            ViewBag.All = "Blog | Random Thoughts";

            ContentsList allcontents = new ContentsList();

            allcontents.post = _db.Post.Where(x => x.Status == "published").Select(all => all).ToList();
            allcontents.stories = _db.Stories.ToList();
            
            ViewBag.text = mainIP;

            return View(allcontents);
        }
        public IActionResult Story()
        {
            ContentsList allcontents = new ContentsList();
            allcontents.post = _db.Post.ToList();
            allcontents.stories = _db.Stories.ToList();
            return View(allcontents);

        }
        public IActionResult Blog()
        {
            ViewBag.Blog = "Blog";
            var result = _db.Post.Where(x => x.Category == "Blog" || x.Category == "Both").Select(all => all).ToList();
            return View("Blog", result);
        }
        public IActionResult Blog_post_read(int id)
        {
            var getCategory = _db.Post.Where(x => x.Id == id).Select(x => x.Category).FirstOrDefault();

            VisitorCounterClass.AddVisitor(id, getCategory, mainIP, _db);

            var count = (from c in _db.Visitor
                         where c.postId == id && c.Point == getCategory
                         select c).Count();

            var entityToUpdate = _db.Post.FirstOrDefault(x => x.Id == id);

            if (entityToUpdate != null)
            {
                entityToUpdate.Views = count;
                _db.Post.Update(entityToUpdate);
                _db.SaveChanges();
            }

            var next = (from a in _db.Post
                        where a.Id > id
                        orderby a.Id
                        select a.Id).FirstOrDefault();

            ViewBag.Next = next;
            ViewBag.Prev = id - 1;

            var result = _db.Post.Where(x => x.Id == id).Select(all => all).ToList();
            return View("Blog_post_read", result);
        }
        public IActionResult Random()
        {
            ViewBag.RandomThoughts = "Random Thoughts";
            var result = _db.Post.Where(x => x.Category == "Random" || x.Category == "Both").Select(all => all).ToList();
            return View("Random", result);
        }

        public IActionResult Part(int? id)
        {
            var count = _db.Chapter.Where(x => x.story_id == id).Select(x => x.Views).DefaultIfEmpty().Sum();

            var entityToUpdate = _db.Stories.FirstOrDefault(x => x.Id == id);

            if (entityToUpdate != null)
            {
                entityToUpdate.Views = count;
                _db.Stories.Update(entityToUpdate);
                _db.SaveChanges();
            }

            List<Posts> postList = _db.Post.ToList();
            ViewBag.post = postList.OrderByDescending(x => x.Id).ToList();

            var query = from s in _db.Stories
                        join c in _db.Chapter on s.Id equals c.story_id into x
                        from c in x.DefaultIfEmpty()
                        where s.Id == id
                        select new Contents { stories = s, chapters = c };
            return View(query);

        }
        public IActionResult Read(int id)
        {
            VisitorCounterClass.AddVisitor(id, "Story", mainIP, _db);

            var count = (from c in _db.Visitor
                         where c.postId == id && c.visitorIp == mainIP && c.Point == "Story"
                         select c).Count();


            var entityToUpdate = _db.Chapter.FirstOrDefault(x => x.Id == id);

            if (entityToUpdate != null)
            {
                entityToUpdate.Views = count;
                _db.Chapter.Update(entityToUpdate);
                _db.SaveChanges();
            }

            var storyID = _db.Chapter.Where(x => x.Id == id).Select(stats => stats.story_id).Single();
            ViewBag.title = _db.Stories.Where(x => x.Id == Convert.ToInt32(storyID)).Select(stats => stats.Title).Single();

            ViewBag.min = (from c in _db.Chapter
                           where c.story_id == Convert.ToInt32(storyID)
                           select c).Min(c => c.Id);

            ViewBag.max = (from c in _db.Chapter
                           where c.story_id == Convert.ToInt32(storyID)
                           select c).Max(c => c.Id);

            var next = (from a in _db.Chapter
                        where a.story_id == Convert.ToInt32(storyID) && a.Id > id
                        orderby a.Id
                        select a.Id).FirstOrDefault();


            ViewBag.Next = next;
            ViewBag.Prev = id - 1;

            var result = _db.Chapter.Where(x => x.Id == id).Select(all => all).ToList();
            return View("Read", result);

        }
        [HttpPost]
        public IActionResult Login(Account accnt)
        {
            var user = _db.Accounts.Where(x => x.Username == accnt.Username && x.Password == accnt.Password).FirstOrDefault();

            if (user != null)
            {
                HttpContext.Session.SetString("Id", accnt.Id.ToString());
                HttpContext.Session.SetString("Username", accnt.Username!.ToString());
                return RedirectToAction("Index", "Admin");
            }
            return View();

        }
    }
}
