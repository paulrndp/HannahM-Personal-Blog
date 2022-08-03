using Microsoft.AspNetCore.Mvc;
using hannahM.Data;

namespace hannahM.ViewComponents
{
    [ViewComponent(Name = "Profile")]
    public class ProfileViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public ProfileViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index",_db.Accounts.ToList());
        }
    }
}
