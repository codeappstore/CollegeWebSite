using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorIndex500()
        {
            return View();
        }
    }
}