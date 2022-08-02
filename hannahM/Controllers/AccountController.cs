using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace hannahM.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Account> acc = _db.Accounts;
            return View(acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Account obj, List<IFormFile> Profile)
        {

            if (ModelState.IsValid)
            {
                Account acc = new Account();
                acc.Firstname = obj.Firstname;
                acc.LastName = obj.LastName;
                acc.Username = obj.Username;
                acc.Password = obj.Password;
                acc.Desc = obj.Desc;

                foreach (var item in Profile)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            acc.Profile = stream.ToArray();
                        }
                    }
                }
                _db.Accounts.Update(acc);
                _db.SaveChanges();
                TempData["success"] = "Profile successfully updated.";

            }
            else
            {
                TempData["error"] = "There was an error submitting this form.";
                return RedirectToAction("Index", obj);
            }
            return RedirectToAction("Index");
        }

    }
}
