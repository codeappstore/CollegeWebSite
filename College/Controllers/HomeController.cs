using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.FrontEndOtherDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace College.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFrontEndRepo _front;
        private readonly ILayoutRepo _layout;
        public HomeController(IFrontEndRepo _front, ILayoutRepo _layout)
        {
            this._front = _front;
            this._layout = _layout;
        }

        private async Task SetLayout()
        {
            var layout = new LayoutModelDto()
            {
                Footer = await _layout.FetchFooterHeaderAsyncTask(1),
                StudentSay = await _layout.FetchStudentsSayAsyncTask(1),
                ImportantLinks = await _layout.FetchImportantLinksListAsyncTask(),
                SalientFeatures = await _layout.FetchSalientFeaturesListAsyncTask(),
                Students = await _layout.FetchStudentsSayingListAsyncTask()
            };
            HttpContext.Session.SetComplexData("_Layout", layout);
        }

        public async Task<IActionResult> Index()
        {
            await SetLayout();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Background()
        {
            return View();
        }

        public IActionResult Forestry()
        {
            return View();
        }
        public IActionResult Agriculture()
        {
            return View();
        }

        public IActionResult Faculties()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
