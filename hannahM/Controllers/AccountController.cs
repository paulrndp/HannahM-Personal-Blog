using hannahM.Data;
using hannahM.Models;
using hannahM.Action;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hannahM.Controllers
{
    //[SessionExpire]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int? id)
        {
            var status = _db.Accounts.Where(x => x.Id == id).Select(stats => stats).First();
            return View("Index", status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Account obj, List<IFormFile> Profile)
        {
            if (Profile.Count == 0)
            {
                _db.Entry(obj).State = EntityState.Modified;
                _db.Entry(obj).Property("Username").IsModified = true;

                if (obj.Password == null)
                {
                    _db.Entry(obj).Property("Password").IsModified = false;
                }
                else
                    _db.Entry(obj).Property("Password").IsModified = true;

                _db.Entry(obj).Property("Firstname").IsModified = true;
                _db.Entry(obj).Property("LastName").IsModified = true;
                _db.Entry(obj).Property("Desc").IsModified = true;
                _db.Entry(obj).Property("Profile").IsModified = false;
                _db.SaveChanges();
                TempData["success"] = "Successfully Updated!";

                return RedirectToAction("Index");
            }
            else
            {

                _db.Entry(obj).State = EntityState.Modified;
                _db.Entry(obj).Property("Username").IsModified = true;
                if (obj.Password == null)
                {
                    _db.Entry(obj).Property("Password").IsModified = false;

                }
                else
                    _db.Entry(obj).Property("Password").IsModified = true;
                _db.Entry(obj).Property("Firstname").IsModified = true;
                _db.Entry(obj).Property("LastName").IsModified = true;
                _db.Entry(obj).Property("Desc").IsModified = true;

                foreach (var item in Profile)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            obj.Profile = stream.ToArray();
                        }
                    }
                }
                _db.Accounts.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Successfully Updated!";

                return RedirectToAction("Index");


            }
        }

    }
}
