using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AcademicItemsDto;
using College.Model.DataTransferObject.CarouselDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.DataTransferObject.PageDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace College.Controllers
{
    [AuthOverride]
    public class PageController : Controller
    {
        private readonly IFrontEndRepo _repo;
        private readonly IWebHostEnvironment _env;
        public PageController(IFrontEndRepo _repo, IWebHostEnvironment _env)
        {
            this._repo = _repo;
            this._env = _env;
        }
        // GET: PageController
        public async Task<IActionResult> Home()
        {
            // Carousel 
            // Homepage from page 

            // Academics
            // Academics Items

            // Page and Academic in page update

            // Carousel Academics Items different page for create update

            var carousel = await _repo.FetchCarouselListAsyncTask();
            var home = await _repo.FetchPageDataByIdAsyncTask(1);
            var academic = await _repo.FetchAcademicDataByIdAsyncTask(1);
            var academicItem = await _repo.FetchAcademicItemListAsyncTask();
            var combinedModel = new PageCarouselAcademicItemsModelDto()
            {
                Carousel = carousel,
                AcademicItems = academicItem,
                Academic = academic,
                Page = home
            };
            return View("Home/Home", combinedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Home(PageCarouselAcademicItemsModelDto combinedModel)
        {
            if (ModelState.IsValid)
            {
                if (combinedModel.Page != null)
                {
                    // Update Image
                    var imageString = "";
                    var pageDetails = await _repo.FetchPageDataByIdAsyncTask(combinedModel.Page.PageId);
                    if (combinedModel.Page.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\" + combinedModel.Page.PageName + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(combinedModel.Page.ImageString.FileName + Path.GetExtension(combinedModel.Page.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await combinedModel.Page.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(pageDetails.BackgroundImage))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Home));
                        }
                        combinedModel.Page.BackgroundImage = imageString;
                    }
                    else
                    {
                        combinedModel.Page.BackgroundImage = pageDetails.BackgroundImage;
                    }


                    // Update in db
                    if (await _repo.UpdatePageDataAsyncTask(combinedModel.Page))
                    {
                        HttpContext.Session.SetString("Success", "Home page updated Successfully.");
                        return RedirectToAction(nameof(Home));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Home));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Home));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcademicDetails(PageCarouselAcademicItemsModelDto combinedModel)
        {
            if (ModelState.IsValid)
            {
                if (combinedModel.Academic != null)
                {
                    // Update in db
                    if (await _repo.UpdateAcademicDataAsyncTask(combinedModel.Academic))
                    {
                        HttpContext.Session.SetString("Success", "Academic summery updated Successfully.");
                        return RedirectToAction(nameof(Home));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Home));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Home));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }

        public async Task<IActionResult> AcademicItem(int id)
        {
            // Academics Items by id
            // Fetch and send to view
            if (id != 0)
            {
                var academicItem = await _repo.FetchAcademicItemByIdAsyncTask(id);
                return View("Home/AcademicSummary", academicItem);
            }
            else
            {
                HttpContext.Session.SetString("Error", "Academic Item not found!");
                return RedirectToAction(nameof(Home));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcademicItem(AcademicItemsModelDto academicItems)
        {
            if (ModelState.IsValid)
            {
                if (academicItems != null)
                {
                    // Update Image
                    var imageString = "";
                    var academicItem = await _repo.FetchAcademicItemByIdAsyncTask(academicItems.ItemId);
                    if (academicItems.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\Academic Item\" + academicItem.Title + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(academicItems.ImageString.FileName + Path.GetExtension(academicItems.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await academicItems.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(academicItem.Image))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Home));
                        }
                        academicItems.Image = imageString;
                    }
                    else
                    {
                        academicItems.Image = academicItem.Image;
                    }


                    // Update in db
                    if (await _repo.UpdateAcademicItemAsyncTask(academicItems))
                    {
                        HttpContext.Session.SetString("Success", "Academic item summary updated Successfully.");
                        return RedirectToAction(nameof(Home));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Home));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Home));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }

        public IActionResult CarouselAdd()
        {
            // Academics Items by id
            var model = new CarouselModelDto();
            return View("Home/CarouselAdd", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarouselAdd(CarouselModelDto carousel)
        {
            if (ModelState.IsValid)
            {
                if (carousel != null)
                {
                    // Update Image
                    var imageString = "";
                    var carouselItem = await _repo.FetchCarouselByIdAsyncTask(carousel.CarouselId);
                    if (carousel.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\Carousel Item\" + carousel.Title + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(carousel.ImageString.FileName + Path.GetExtension(carousel.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await carousel.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(carouselItem.Image))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Home));
                        }
                        carousel.Image = imageString;
                    }
                    else
                    {
                        carousel.Image = carouselItem.Image;
                    }


                    // Update in db
                    if (await _repo.CreateCarouselAsyncTask(carousel))
                    {
                        HttpContext.Session.SetString("Success", "Carousel item added Successfully.");
                        return RedirectToAction(nameof(Home));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Home));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Home));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }


        public async Task<IActionResult> CarouselUpdate(int id)
        {
            if (id != 0)
            {
                // Academics Items by id
                var model = await _repo.FetchCarouselByIdAsyncTask(id);
                return View("Home/CarouselUpdate", model);
            }
            else
            {
                HttpContext.Session.SetString("Error", "Carousel Item not found!");
                return RedirectToAction(nameof(Home));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarouselUpdate(CarouselModelDto carousel)
        {
            if (ModelState.IsValid)
            {
                if (carousel != null)
                {
                    // Update Image
                    var imageString = "";
                    var carouselItem = await _repo.FetchCarouselByIdAsyncTask(carousel.CarouselId);
                    if (carousel.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\Carousel Item\" + carousel.Title + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(carousel.ImageString.FileName + Path.GetExtension(carousel.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await carousel.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(carouselItem.Image))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Home));
                        }
                        carousel.Image = imageString;
                    }
                    else
                    {
                        carousel.Image = carouselItem.Image;
                    }


                    // Update in db
                    if (await _repo.UpdateCarouselAsyncTask(carousel))
                    {
                        HttpContext.Session.SetString("Success", "Carousel item  updated Successfully.");
                        return RedirectToAction(nameof(Home));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Home));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Home));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CarouselDelete(int id)
        {

            var dataSet = await _repo.FetchCarouselByIdAsyncTask(id);
            // Delete User image folder
            // Users Folder
            var userFolderPath = @"\User_Information\Pages\Carousel Item\" + dataSet.Title + @"\";
            // Root Path
            var webRootPath = _env.WebRootPath;
            // Base Path
            var basePath = Path.Combine(webRootPath + userFolderPath);
            // Base Path Exists or create new base path
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (basePathExists) Directory.Delete(basePath, true);

            // Delete user
            if (await _repo.DeleteCarouselAsyncTask(dataSet.CarouselId))
            {
                return Json("Success, Carousel deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }

        public async Task<IActionResult> AboutUs()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask(2);
            return View("AboutUs/AboutUs", pages);
        }

        public async Task<IActionResult> Background()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask(3);
            return View("Background/Background", pages);
        }

        public async Task<IActionResult> Forestry()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask(4);
            return View("Forestry/Forestry", pages);
        }


        public async Task<IActionResult> Agriculture()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask(5);
            return View("Agriculture/Agriculture", pages);
        }


        public async Task<IActionResult> Privacy()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask(6);
            return View("Privacy/Privacy", pages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePage(PageModelDto page)
        {
            if (ModelState.IsValid)
            {
                if (page != null)
                {
                    // Update Image
                    var imageString = "";
                    var pageDetails = await _repo.FetchPageDataByIdAsyncTask(page.PageId);
                    if (page.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\" + page.PageName + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(page.ImageString.FileName + Path.GetExtension(page.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await page.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(pageDetails.BackgroundImage))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction("Index", "WebSite");
                        }
                        page.BackgroundImage = imageString;
                    }
                    else
                    {
                        page.BackgroundImage = pageDetails.BackgroundImage;
                    }


                    // Update in db
                    if (await _repo.UpdatePageDataAsyncTask(page))
                    {
                        HttpContext.Session.SetString("Success", page.PageName + " page updated Successfully.");
                        return RedirectToAction("Index", "WebSite");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction("Index", "WebSite");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction("Index", "WebSite");
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction("Index", "WebSite");
            }
        }
    }
}
