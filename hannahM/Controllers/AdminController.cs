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
            ViewBag.B = _db.Blog.Count();
            ViewBag.S = _db.Stories.Count();
            ViewBag.R = _db.Random.Count();
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
    }
}