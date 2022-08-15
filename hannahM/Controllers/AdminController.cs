using hannahM.Data;
using hannahM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using hannahM.Action;

namespace hannahM.Controllers
{
    //[SessionExpire]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        public void countTables()
        {
            ViewBag.S = _db.Stories.Count();
            ViewBag.B = _db.Post.Where(x => x.Category == "Blog" || x.Category == "Both" && x.Status == "published").Count();

            ViewBag.BDraft = _db.Post.Where(x => x.Category == "Blog" && x.Status.Contains("draft") || x.Category.Contains("Both")).Count();

            ViewBag.R = _db.Post.Where(x => x.Category == "Random" && x.Status == "published").Count();


            ViewBag.RDraft = _db.Post.Where(x => x.Category == "Random" && x.Status.Contains("draft") || x.Category.Contains("Both")).Count();

            ViewBag.draft = _db.Post.Where(x => x.Status == "draft").Count();
        }
        public IActionResult Index()
        {
            countTables();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Main");
        }
    }
}