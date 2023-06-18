using Microsoft.AspNetCore.Mvc;

namespace Greeny.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
