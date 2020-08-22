using College.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    [AuthOverride]
    public class ControlPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
