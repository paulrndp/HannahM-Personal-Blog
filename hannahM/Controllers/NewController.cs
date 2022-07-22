using Microsoft.AspNetCore.Mvc;

namespace hannahM.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
