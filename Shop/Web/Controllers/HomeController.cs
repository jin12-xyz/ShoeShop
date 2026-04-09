using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
