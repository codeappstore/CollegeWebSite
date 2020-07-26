using CollegeWebsite.Override;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollegeWebsite.Controllers
{
    [AuthOverride]
    public class ControlPanelController : Controller
    {
        private readonly ILogger<ControlPanelController> _logger;

        public ControlPanelController(ILogger<ControlPanelController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
