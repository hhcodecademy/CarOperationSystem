using Microsoft.AspNetCore.Mvc;

namespace CarOperationSystem.UI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string fullname = Request.Cookies["fullname"];
            ViewBag.fullname = fullname;
            return View();
        }
    }
}
