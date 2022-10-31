using Microsoft.AspNetCore.Mvc;

namespace hannahM.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
