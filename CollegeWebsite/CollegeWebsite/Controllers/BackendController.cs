using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollegeWebsite.Controllers
{
    public class BackendController : Controller
    {
        private readonly ILogger<BackendController> _logger;

        public BackendController(ILogger<BackendController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
