using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
