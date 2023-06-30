using Microsoft.AspNetCore.Mvc;

namespace Greeny.Controllers
{
    public class ContactController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
