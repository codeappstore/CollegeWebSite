using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.FrontEndOtherDto;
using College.Model.DataTransferObject.PageDto;
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
                Footer = await _layout.FetchFooterHeaderAsyncTask((int)Enums.Page.Default),
                StudentSay = await _layout.FetchStudentsSayAsyncTask((int)Enums.Page.Default),
                ImportantLinks = await _layout.FetchImportantLinksListAsyncTask(),
                SalientFeatures = await _layout.FetchSalientFeaturesListAsyncTask(),
                Students = await _layout.FetchStudentsSayingListAsyncTask()
            };
            HttpContext.Session.SetComplexData("_Layout", layout);
        }

        private async Task<PageModelDto> FetchPage(Enums.Page page)
        {
            return await _front.FetchPageDataByIdAsyncTask((int)page);
        }

        public async Task<IActionResult> Index()
        {
            var homeDataSet = new HomepageModelDto()
            {
                Academic = await _front.FetchAcademicDataByIdAsyncTask((int)Enums.Page.Default),
                Page = await FetchPage(Enums.Page.Home),
                PopUp = await _front.FetchPopUpByIdAsyncTask((int)Enums.Page.Default),
                AcademicItems = await _front.FetchAcademicItemListAsyncTask(),
                Carousel = await _front.FetchCarouselListAsyncTask()
            };
            await SetLayout();
            HttpContext.Session.SetComplexData("_Index", homeDataSet);
            return View();
        }

        public async Task<IActionResult> About()
        {
            await SetLayout();
            var about = await _front.FetchPageDataByIdAsyncTask((int)Enums.Page.About);
            HttpContext.Session.SetComplexData("_About", about);
            return View();
        }

        public async Task<IActionResult> Background()
        {
            await SetLayout();
            var background = await _front.FetchPageDataByIdAsyncTask((int)Enums.Page.Background);
            HttpContext.Session.SetComplexData("_Background", background);
            return View();
        }

        public async Task<IActionResult> Forestry()
        {
            await SetLayout();
            var combinedModel = new PageAttachmentModelDto()
            {
                Page = await _front.FetchPageDataByIdAsyncTask((int)Enums.Page.Forestry),
                Attachment = await _front.FetchAttachmentByIdAsyncTask((int)Enums.Page.Forestry)
            };
            HttpContext.Session.SetComplexData("_Forestry", combinedModel);
            return View();
        }
        public async Task<IActionResult> Agriculture()
        {
            await SetLayout();
            var combinedModel = new PageAttachmentModelDto()
            {
                Page = await _front.FetchPageDataByIdAsyncTask((int)Enums.Page.Agriculture),
                Attachment = await _front.FetchAttachmentByIdAsyncTask((int)Enums.Page.Agriculture)
            };
            HttpContext.Session.SetComplexData("_Agriculture", combinedModel);
            return View();
        }

        public async Task<IActionResult> Faculties()
        {
            await SetLayout();
            var combinedModel = new PageTeacherModelDto()
            {
                Page = await _front.FetchPageDataByIdAsyncTask((int)Enums.Page.Staff),
                Teacher = await _front.FetchTeacherListAsyncTask()
            };
            HttpContext.Session.SetComplexData("_Faculties", combinedModel);
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await SetLayout();
            var privacy = await _front.FetchPageDataByIdAsyncTask((int)Enums.Page.Privacy);
            HttpContext.Session.SetComplexData("_Privacy", privacy);
            return View();
        }

    }
}
