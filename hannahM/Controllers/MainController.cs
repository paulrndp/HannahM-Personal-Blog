using hannahM.Data;
using hannahM.Models;
using Microsoft.AspNetCore.Mvc;



namespace hannahM.Controllers
{

    public class MainController : Controller
    {

        private readonly ApplicationDbContext _db;


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
            ViewBag.Blog = "Blog";
            ViewBag.Stories = "Stories";
            ViewBag.RandomThoughts = "Random Thoughts";

            ContentsList allcontents = new ContentsList();
            allcontents.blogs = _db.Blog.ToList();
            allcontents.random = _db.Random.ToList();
            allcontents.stories = _db.Stories.ToList();
            allcontents.chapters = _db.Chapter.ToList();

            return View(allcontents);
        }
        public IActionResult Story()
        {
            ContentsList allcontents = new ContentsList();
            allcontents.blogs = _db.Blog.ToList();
            allcontents.random = _db.Random.ToList();
            allcontents.stories = _db.Stories.ToList();
            allcontents.chapters = _db.Chapter.ToList();
            return View(allcontents);

        }
        public IActionResult Blog()
        {

            ViewBag.Blog = "Blog";
            var result = _db.Blog.Select(all => all).ToList();
            return View("Blog", result);
        }
        public IActionResult Blog_post_read(int? id)
        {
            var result = _db.Blog.Where(x => x.Id == id).Select(all => all).ToList();
            return View("Blog_post_read", result);
        }
        public IActionResult Random_post_read(int? id)
        {
            var result = _db.Random.Where(x => x.Id == id).Select(all => all).ToList();
            return View("Random_post_read", result);
        }

        public IActionResult Random()
        {
            ViewBag.RandomThoughts = "Random Thoughts";
            var result = _db.Random.Select(all => all).ToList();
            return View("Random", result);
        }

        public IActionResult Part(int? id)
        {

            List<Blog> blogList = _db.Blog.ToList();
            List<RandomThoughts> randomList = _db.Random.ToList();
            ViewBag.rndm = randomList.OrderByDescending(x => x.Id).ToList();
            ViewBag.blg = blogList.OrderByDescending(x => x.Id).ToList();

            var query = from s in _db.Stories
                        join c in _db.Chapter on s.Id equals c.story_id into x
                        from c in x.DefaultIfEmpty()
                        where s.Id == id
                        select new Contents { stories = s, chapters = c };
            return View(query);

        }

        public IActionResult Read(int? id)
        {

            var storyID = _db.Chapter.Where(x => x.Id == id).Select(stats => stats.story_id).Single();
            ViewBag.title = _db.Stories.Where(x => x.Id == Convert.ToInt32(storyID)).Select(stats => stats.Title).Single();

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
                HttpContext.Session.SetString("Username", accnt.Username.ToString());
                return RedirectToAction("Index", "Admin");
            }
            return View();

        }
    }
}
