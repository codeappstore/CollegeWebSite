using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AcademicItemsDto;
using College.Model.DataTransferObject.CarouselDto;
using College.Model.DataTransferObject.DownloadsDto;
using College.Model.DataTransferObject.GalleryDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.DataTransferObject.PageDto;
using College.Model.DataTransferObject.PopupDto;
using College.Model.DataTransferObject.TeacherDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using GalleryModelDto = College.Model.DataTransferObject.GalleryDto.GalleryModelDto;

namespace College.Controllers
{
    [AuthOverride]
    public class PageController : Controller
    {
        private readonly IFrontEndRepo _repo;
        private readonly IWebHostEnvironment _env;
        private readonly IDownloadsRepo _downloads;
        private readonly IGalleryRepo _gallery;
        public PageController(IFrontEndRepo repo, IWebHostEnvironment env, IDownloadsRepo downloads, IGalleryRepo gallery)
        {
            _repo = repo;
            _env = env;
            _downloads = downloads;
            _gallery = gallery;
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

            var combinedModel = new PageCarouselAcademicItemsModelDto()
            {
                Carousel = await _repo.FetchCarouselListAsyncTask(),
                AcademicItems = await _repo.FetchAcademicItemListAsyncTask(),
                Academic = await _repo.FetchAcademicDataByIdAsyncTask((int)Enums.Page.Default),
                Page = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Default),
                Brochure = await _repo.FetchAttachmentByIdAsyncTask((int)Enums.Page.Home)
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
                            return RedirectToAction(nameof(AcademicItem));
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
                        return RedirectToAction(nameof(AcademicItem));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(AcademicItem));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(AcademicItem));
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
            var pages = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.About);
            return View("AboutUs/AboutUs", pages);
        }

        public async Task<IActionResult> Mayor()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Mayor);
            return View("Mayor/Mayor", pages);
        }

        public async Task<IActionResult> Background()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Background);
            return View("Background/Background", pages);
        }

        public async Task<IActionResult> Forestry()
        {
            var combinedModel = new AttachmentPageModel()
            {
                Attachment = await _repo.FetchAttachmentByIdAsyncTask((int)Enums.Page.Forestry),
                Page = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Forestry)
            };
            return View("Forestry/Forestry", combinedModel);
        }

        public async Task<IActionResult> Agriculture()
        {
            var combinedModel = new AttachmentPageModel()
            {
                Attachment = await _repo.FetchAttachmentByIdAsyncTask((int)Enums.Page.Agriculture),
                Page = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Agriculture)
            };
            return View("Agriculture/Agriculture", combinedModel);
        }

        public async Task<IActionResult> Privacy()
        {
            var pages = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Privacy);
            return View("Privacy/Privacy", pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePageWithAttachment(AttachmentPageModel combinedModel)
        {
            if (ModelState.IsValid)
            {
                if (combinedModel.Page != null)
                {
                    // Update Image
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
                        var imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(pageDetails.BackgroundImage))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction("Index", "WebSite");
                        }
                        combinedModel.Page.BackgroundImage = imageString;
                    }
                    else
                    {
                        combinedModel.Page.BackgroundImage = pageDetails.BackgroundImage;
                    }

                    if (combinedModel.Attachment != null)
                    {
                        var fileDetails = await _repo.FetchAttachmentByIdAsyncTask(combinedModel.Page.PageId);
                        if (combinedModel.Attachment.FileString != null)
                        {
                            var userFilePath = @"\User_Information\Pages\" + combinedModel.Page.PageName + @"\";
                            // Root Path
                            var webRootPath = _env.WebRootPath;
                            // Base Path
                            var basePath = Path.Combine(webRootPath + userFilePath);
                            // Base Path Exists or create new base path
                            bool basePathExists = System.IO.Directory.Exists(basePath);
                            if (!basePathExists) Directory.CreateDirectory(basePath);
                            // File
                            var fileName = Path.GetFileNameWithoutExtension(combinedModel.Attachment.FileString.FileName + Path.GetExtension(combinedModel.Attachment.FileString.FileName));
                            var filePath = Path.Combine(basePath, fileName);
                            var fileExists = System.IO.File.Exists(filePath);
                            if (fileExists) System.IO.File.Delete(filePath);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await combinedModel.Attachment.FileString.CopyToAsync(stream);
                            }
                            var fileString = userFilePath + fileName;
                            if (string.IsNullOrWhiteSpace(fileString) && string.IsNullOrWhiteSpace(fileDetails.FileName))
                            {
                                HttpContext.Session.SetString("Error", "File is required!");
                                return RedirectToAction("Index", "WebSite");
                            }
                            combinedModel.Attachment.Link = fileString;
                        }
                        else
                        {
                            combinedModel.Attachment.Link = fileDetails.Link;
                        }
                        // Update in db
                        if (await _repo.UpdateAttachmentAsyncTask(combinedModel.Attachment))
                        {
                            HttpContext.Session.SetString("Success", combinedModel.Attachment.FileName + " page updated Successfully.");
                        }
                        else
                        {
                            HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        }
                    }


                    // Update in db
                    if (await _repo.UpdatePageDataAsyncTask(combinedModel.Page))
                    {
                        HttpContext.Session.SetString("Success", combinedModel.Page.PageName + " page updated Successfully.");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAttachment(PageCarouselAcademicItemsModelDto combinedModel)
        {
            if (ModelState.IsValid)
            {
                if (combinedModel.Brochure != null)
                {
                    var fileDetails = await _repo.FetchAttachmentByIdAsyncTask(combinedModel.Brochure.PageId);
                    if (combinedModel.Brochure.FileString == null)
                        combinedModel.Brochure.Link = fileDetails.Link;
                    else
                    {
                        var userFilePath = @"\User_Information\Pages\" + combinedModel.Brochure.FileName + @"\";
                        // Base Path
                        var basePath = Path.Combine(_env.WebRootPath + userFilePath);
                        // Base Path Exists or create new base path
                        if (!System.IO.Directory.Exists(basePath)) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(combinedModel.Brochure.FileString.FileName + Path.GetExtension(combinedModel.Brochure.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await combinedModel.Brochure.FileString.CopyToAsync(stream);
                        }
                        var fileString = userFilePath + fileName;
                        if (string.IsNullOrWhiteSpace(fileString) && string.IsNullOrWhiteSpace(fileDetails.Link))
                        {
                            HttpContext.Session.SetString("Error", "File is required!");
                            return RedirectToAction("Home", "Page");
                        }
                        combinedModel.Brochure.Link = fileString;

                    }

                    // Update in db
                    if (await _repo.UpdateAttachmentAsyncTask(combinedModel.Brochure))
                        HttpContext.Session.SetString("Success", combinedModel.Brochure.FileName + " updated Successfully.");
                    else
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction("Home", "Page");
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction("Home", "Page");
            }

            return null;
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
                        var imageString = userImagePath + fileName;
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

        public async Task<IActionResult> Staffs()
        {
            var comboModel = new StaffPagesModelDto()
            {
                Page = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Staff),
                Staff = await _repo.FetchTeacherListAsyncTask()
            };
            return View("Staffs/Staffs", comboModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Staffs(StaffPagesModelDto combinedModel)
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
                            return RedirectToAction(nameof(Staffs));
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
                        HttpContext.Session.SetString("Success", "Staffs and faculties page updated Successfully.");
                        return RedirectToAction(nameof(Staffs));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Staffs));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Staffs));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Staffs));
            }
        }

        public IActionResult StaffsAdd()
        {
            var staffModel = new TeacherModelDto();
            return View("Staffs/StaffAdd", staffModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StaffsAdd(TeacherModelDto staff)
        {
            if (ModelState.IsValid)
            {
                if (staff != null)
                {
                    // Update Image
                    var imageString = "";
                    if (staff.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Staffs\" + staff.TeacherName + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(staff.ImageString.FileName + Path.GetExtension(staff.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await staff.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Staffs));
                        }
                        staff.Image = imageString;
                    }


                    // Update in db
                    if (await _repo.CreateTeacherAsyncTask(staff))
                    {
                        HttpContext.Session.SetString("Success", "Staff added Successfully.");
                        return RedirectToAction(nameof(Staffs));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Staffs));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Staffs));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }

        public async Task<IActionResult> StaffsUpdate(int id)
        {
            if (id != 0)
            {
                var staffModel = await _repo.FetchTeacherByIdAsyncTask(id);
                return View("Staffs/StaffUpdate", staffModel);
            }
            else
            {
                HttpContext.Session.SetString("Error", "Staff not found!");
                return RedirectToAction(nameof(Staffs));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StaffsUpdate(TeacherModelDto staff)
        {
            if (ModelState.IsValid)
            {
                if (staff != null)
                {
                    // Update Image
                    var imageString = "";
                    var teacher = await _repo.FetchTeacherByIdAsyncTask(staff.TeacherId);
                    if (staff.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Staffs\" + staff.TeacherName + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(staff.ImageString.FileName + Path.GetExtension(staff.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await staff.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(teacher.Image))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Staffs));
                        }
                        staff.Image = imageString;
                    }
                    else
                    {
                        staff.Image = teacher.Image;
                    }


                    // Update in db
                    if (await _repo.UpdateTeacherAsyncTask(staff))
                    {
                        HttpContext.Session.SetString("Success", "Staffs and faculties page updated Successfully.");
                        return RedirectToAction(nameof(Staffs));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Staffs));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Staffs));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Staffs));
            }
        }

        [HttpPost]
        public async Task<IActionResult> StaffsDelete(int id)
        {
            var dataSet = await _repo.FetchTeacherByIdAsyncTask(id);
            // Delete User image folder
            // Users Folder
            var userFolderPath = @"\User_Information\Staffs\" + dataSet.TeacherName + @"\";
            // Root Path
            var webRootPath = _env.WebRootPath;
            // Base Path
            var basePath = Path.Combine(webRootPath + userFolderPath);
            // Base Path Exists or create new base path
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (basePathExists) Directory.Delete(basePath, true);

            // Delete user
            if (await _repo.DeleteTeacherAsyncTask(dataSet.TeacherId))
            {
                return Json("Success, Staff deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }


        public async Task<IActionResult> Popup()
        {
            var popup = await _repo.FetchPopUpByIdAsyncTask((int)Enums.Page.Default);
            return View("Popup/Popup", popup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Popup(PopupModelDto popup)
        {
            if (ModelState.IsValid)
            {
                if (popup != null)
                {
                    // Update Image
                    var imageString = "";
                    if (popup.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Popup\" + popup.Name + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(popup.ImageString.FileName + Path.GetExtension(popup.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await popup.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Staffs));
                        }
                        popup.Link = imageString;
                    }


                    // Update in db
                    if (await _repo.UpdatePopUpAsyncTask(popup))
                    {
                        HttpContext.Session.SetString("Success", "Popup added Successfully.");
                        return RedirectToAction(nameof(Popup));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Staffs));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Staffs));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Home));
            }
        }

        public async Task<IActionResult> Downloads()
        {
            var combinedModel = new DownloadsPageModelDto()
            {
                Page = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Downloads),
                Downloads = await _downloads.FetchAllDownloadsAsyncTask()

            };
            return View("Downloads/Downloads", combinedModel);
        }

        public IActionResult DownloadsAdd()
        {
            var model = new DownloadModelDto();
            return View("Downloads/DownloadsAdd", model);
        }

        public async Task<IActionResult> DownloadsUpdate(int id)
        {
            var model = await _downloads.FetchDownloadAsyncTask(id);
            return View("Downloads/DownloadsUpdate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadsAdd(DownloadModelDto download)
        {
            if (ModelState.IsValid)
            {
                if (download != null)
                {
                    if (download.FileString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Downloads\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var extension = Path.GetExtension(download.FileString.FileName);
                        var size = (download.FileString.Length / 1024f) + " KB";
                        var fileName = Path.GetFileNameWithoutExtension(download.FileString.FileName + Path.GetExtension(download.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await download.FileString.CopyToAsync(stream);
                        }
                        var imageString = userImagePath + fileName;
                        download.FileLink = imageString;
                        download.Size = size;
                        download.Extension = extension;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(DownloadsAdd));
                        }
                    }

                    // Add in db
                    if (await _downloads.CreateDownloadAsyncTask(download))
                    {
                        HttpContext.Session.SetString("Success", "Download item added Successfully.");
                        return RedirectToAction(nameof(Downloads));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(Downloads));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(DownloadsAdd));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(DownloadsAdd));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DownloadsUpdate(DownloadModelDto download)
        {
            if (ModelState.IsValid)
            {
                if (download != null)
                {
                    var fileItem = await _downloads.FetchDownloadAsyncTask(download.FileId);
                    if (download.FileString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Downloads\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var extension = Path.GetExtension(download.FileString.FileName);
                        var size = (download.FileString.Length / 1024f) + " KB";

                        var fileName = Path.GetFileNameWithoutExtension(download.FileString.FileName + Path.GetExtension(download.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await download.FileString.CopyToAsync(stream);
                        }
                        var imageString = userImagePath + fileName;
                        download.FileLink = imageString;
                        download.Size = size;
                        download.Extension = extension;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(DownloadsUpdate));
                        }

                        var deletePath = fileItem.FileLink;
                        var deleteBasePath = Path.Combine(webRootPath + deletePath);
                        if (System.IO.File.Exists(deleteBasePath))
                        {
                            System.IO.File.Delete(deleteBasePath);
                        }

                    }
                    else
                    {
                        download.FileLink = fileItem.FileLink;
                        download.Size = fileItem.Size;
                        download.Extension = fileItem.Extension;
                    }

                    // Add in db
                    if (await _downloads.UpdateDownloadAsyncTask(download))
                    {
                        HttpContext.Session.SetString("Success", "Download item updated Successfully.");
                        return RedirectToAction(nameof(Downloads));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(DownloadsUpdate));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(DownloadsUpdate));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(DownloadsUpdate));
            }
        }


        [HttpPost]
        public async Task<IActionResult> DownloadsDelete(int id)
        {
            var dataSet = await _downloads.FetchDownloadAsyncTask(id);
            // Delete User image folder

            // Root Path
            var webRootPath = _env.WebRootPath;

            var deletePath = dataSet.FileLink;
            var deleteBasePath = Path.Combine(webRootPath + deletePath);
            if (System.IO.File.Exists(deleteBasePath))
            {
                System.IO.File.Delete(deleteBasePath);
            }

            // Delete user
            if (await _downloads.DeleteDownloadAsyncTask(id))
            {
                return Json("Success, File deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }

        public async Task<IActionResult> Gallery()
        {
            var combinedModel = new PageGalleryModelDto()
            {
                Page = await _repo.FetchPageDataByIdAsyncTask((int)Enums.Page.Gallery),
                Gallery = await _gallery.FetchAllGalleryAsyncTask()
            };
            return View("Gallery/Gallery", combinedModel);
        }


        public IActionResult GalleryAdd()
        {
            var model = new GalleryModelDto();
            return View("Gallery/GalleryAdd", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalleryAdd(GalleryModelDto gallery)
        {
            if (ModelState.IsValid)
            {
                if (gallery != null)
                {
                    if (gallery.FileString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Gallery\" + gallery.Title + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(gallery.FileString.FileName + Path.GetExtension(gallery.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await gallery.FileString.CopyToAsync(stream);
                        }
                        var imageString = userImagePath + fileName;
                        gallery.Thumbnail = imageString;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(Gallery));
                        }
                    }

                    // Add in db
                    if (await _gallery.CreateGalleryAsyncTask(gallery))
                    {
                        HttpContext.Session.SetString("Success", "Gallery item added Successfully.");
                        return RedirectToAction(nameof(Gallery));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(GalleryAdd));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(GalleryAdd));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(GalleryAdd));
            }
        }


        public async Task<IActionResult> GalleryUpdate(int id)
        {
            var combinedModel = new GalleryImageModelDto()
            {
                Gallery = await _gallery.FetchGalleryAsyncTask(id),
                Images = await _gallery.FetchAllImageByGalleryIdAsyncTask(id)
            };

            HttpContext.Session.SetInt32("_GalleryID", id);
            return View("Gallery/GalleryUpdate", combinedModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GalleryUpdate(GalleryModelDto gallery)
        {
            if (ModelState.IsValid)
            {
                if (gallery != null)
                {
                    var fileItem = await _gallery.FetchGalleryAsyncTask(gallery.GalleryId);
                    if (gallery.FileString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Gallery\" + gallery.Title + @"\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File

                        var fileName = Path.GetFileNameWithoutExtension(gallery.FileString.FileName + Path.GetExtension(gallery.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await gallery.FileString.CopyToAsync(stream);
                        }
                        var imageString = userImagePath + fileName;
                        gallery.Thumbnail = imageString;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(GalleryUpdate), new { id = gallery.GalleryId });
                        }

                        var deletePath = fileItem.Thumbnail;
                        var deleteBasePath = Path.Combine(webRootPath + deletePath);
                        if (System.IO.File.Exists(deleteBasePath))
                        {
                            System.IO.File.Delete(deleteBasePath);
                        }

                    }
                    else
                    {
                        gallery.Thumbnail = fileItem.Thumbnail;
                    }

                    // Add in db
                    if (await _gallery.UpdateGalleryAsyncTask(gallery))
                    {
                        HttpContext.Session.SetString("Success", "Gallery updated Successfully.");
                        return RedirectToAction(nameof(Gallery));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(GalleryUpdate), new { id = gallery.GalleryId });
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(GalleryUpdate), new { id = gallery.GalleryId });
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(GalleryUpdate), new { id = gallery.GalleryId });
            }
        }


        [HttpPost]
        public async Task<IActionResult> GalleryDelete(int id)
        {
            var dataSet = await _gallery.FetchGalleryAsyncTask(id);
            // Delete Gallery image folder

            var deletePath = @"\User_Information\Gallery\" + dataSet.Title + @"\";

            var webRootPath = _env.WebRootPath;
            // Base Path
            var basePath = Path.Combine(webRootPath + deletePath);


            var images = await _gallery.FetchAllImageByGalleryIdAsyncTask(id);

            if (images.Count > 0)
            {
                foreach (var item in images)
                {
                    var imageBasePath = Path.Combine(webRootPath + item.ImageLink);
                    if (System.IO.File.Exists(imageBasePath))
                    {
                        System.IO.File.Delete(imageBasePath);
                    }
                }
            }


            // Base Path Exists or create new base path
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (basePathExists) Directory.Delete(basePath, true);

            // Delete user
            if (await _gallery.DeleteGalleryAsyncTask(id))
            {
                return Json("Success, File deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }


        public IActionResult ImageAdd()
        {
            var images = new ImageModelDto();
            return View("Gallery/ImageAdd", images);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImageAdd(ImageModelDto image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.FileString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Gallery\Images\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var extension = Path.GetExtension(image.FileString.FileName);
                        var size = (image.FileString.Length / 1024f) + " KB";
                        var fileName = Path.GetFileNameWithoutExtension(image.FileString.FileName + Path.GetExtension(image.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.FileString.CopyToAsync(stream);
                        }
                        var imageString = userImagePath + fileName;
                        image.ImageLink = imageString;
                        image.Size = size;
                        image.Extension = extension;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(ImageAdd));
                        }
                    }

                    // Add in db
                    if (await _gallery.CreateImageAsyncTask(image))
                    {
                        HttpContext.Session.SetString("Success", "Image item added Successfully.");
                        return RedirectToAction(nameof(GalleryUpdate), new { id = image.GalleryId });
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(GalleryUpdate), new { id = image.GalleryId });
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(ImageAdd));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(ImageAdd));
            }
        }


        public async Task<IActionResult> ImageUpdate(int id)
        {
            var combinedModel = await _gallery.FetchImageAsyncTask(id);
            return View("Gallery/ImageUpdate", combinedModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImageUpdate(ImageModelDto image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var fileItem = await _gallery.FetchImageAsyncTask(image.ImageId);
                    if (image.FileString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Gallery\Images\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File

                        // File
                        var extension = Path.GetExtension(image.FileString.FileName);
                        var size = (image.FileString.Length / 1024f) + " KB";

                        var fileName = Path.GetFileNameWithoutExtension(image.FileString.FileName + Path.GetExtension(image.FileString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.FileString.CopyToAsync(stream);
                        }
                        var imageString = userImagePath + fileName;
                        image.ImageLink = imageString;
                        image.Extension = extension;
                        image.Size = size;
                        if (string.IsNullOrWhiteSpace(imageString))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(ImageAdd));
                        }

                        var deletePath = fileItem.ImageLink;
                        var deleteBasePath = Path.Combine(webRootPath + deletePath);
                        if (System.IO.File.Exists(deleteBasePath))
                        {
                            System.IO.File.Delete(deleteBasePath);
                        }
                    }
                    else
                    {
                        image.ImageLink = fileItem.ImageLink;
                        image.Size = fileItem.Size;
                        image.Extension = fileItem.Extension;
                    }

                    // Add in db
                    if (await _gallery.UpdateImageAsyncTask(image))
                    {
                        HttpContext.Session.SetString("Success", "Image updated Successfully.");
                        return RedirectToAction(nameof(GalleryUpdate), new { id = image.GalleryId });
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(GalleryUpdate), new { id = image.GalleryId });
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(ImageAdd));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(ImageAdd));
            }
        }


        [HttpPost]
        public async Task<IActionResult> ImageDelete(int id)
        {
            var dataSet = await _gallery.FetchImageAsyncTask(id);
            // Delete User image folder

            // Root Path
            var webRootPath = _env.WebRootPath;

            var deletePath = dataSet.ImageLink;
            var deleteBasePath = Path.Combine(webRootPath + deletePath);
            if (System.IO.File.Exists(deleteBasePath))
            {
                System.IO.File.Delete(deleteBasePath);
            }

            // Delete user
            if (await _gallery.DeleteImageAsyncTask(id))
            {
                return Json("Success, File deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }
    }
}
