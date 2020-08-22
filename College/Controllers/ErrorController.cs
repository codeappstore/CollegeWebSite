using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
