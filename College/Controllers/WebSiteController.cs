using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.FooterDto;
using College.Model.DataTransferObject.ImportantLinksDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.DataTransferObject.SalientFeaturesDto;
using College.Model.DataTransferObject.StudentSayStudents;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace College.Controllers
{
    [AuthOverride]
    public class WebSiteController : Controller
    {
        private readonly ILayoutRepo _repo;
        private readonly IWebHostEnvironment _env;
        public WebSiteController(ILayoutRepo _repo, IWebHostEnvironment _env)
        {
            this._repo = _repo;
            this._env = _env;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _repo.FetchFooterStudentSlogan();
            return View(data);
        }

        #region Salient Features

        public async Task<IActionResult> Features()
        {
            var dataSet = await _repo.FetchSalientFeaturesListAsyncTask();
            return View("Feature/Features", dataSet);
        }

        public IActionResult FeatureCreate()
        {
            var featureModel = new SalientFeaturesModelDto();
            return View("Feature/FeatureCreate", featureModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeatureCreate(SalientFeaturesModelDto featuresModel)
        {
            if (ModelState.IsValid)
            {
                if (featuresModel != null)
                {
                    if (await _repo.CreateSalientFeaturesAsyncTask(featuresModel))
                    {
                        HttpContext.Session.SetString("Success", "Feature Created Successfully.");
                        return RedirectToAction(nameof(Features));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(Features));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Features));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Features));
            }
        }

        public async Task<IActionResult> FeaturesUpdate(int id)
        {
            var dataSet = await _repo.FetchSalientFeaturesByIdAsyncTask(id);
            return View("Feature/FeaturesUpdate", dataSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeaturesUpdate(SalientFeaturesModelDto featuresModel)
        {
            if (ModelState.IsValid)
            {
                if (featuresModel != null)
                {
                    if (await _repo.UpdateSalientFeaturesAsyncTask(featuresModel))
                    {
                        HttpContext.Session.SetString("Success", "Feature Updated Successfully.");
                        return RedirectToAction(nameof(Features));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Features));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Features));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Features));
            }
        }

        [HttpPost]
        public async Task<IActionResult> FeaturesDelete(int id)
        {

            var dataSet = await _repo.FetchSalientFeaturesByIdAsyncTask(id);

            // Delete user
            if (await _repo.DeleteSalientFeaturesAsyncTask(dataSet.SalientFeatureId))
            {
                return Json("Success, Salient Feature deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }

        #endregion

        #region Footer

        public async Task<IActionResult> Footer()
        {
            var combinedModel = new FooterImportantLinkModelDto()
            {
                FooterUpdateModel = await _repo.FetchFooterHeaderAsyncTask(1),
                ImportantLinksModel = await _repo.FetchImportantLinksListAsyncTask()
            };
            return View("Footer/Footer", combinedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFooter(FooterUpdateModelDto footerUpdate)
        {
            if (ModelState.IsValid)
            {
                if (footerUpdate != null)
                {
                    if (await _repo.UpdateFooterHeaderAsyncTask(footerUpdate))
                    {
                        HttpContext.Session.SetString("Success", "Footer details Updated Successfully.");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Footer));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Footer));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Footer));
            }
        }

        public IActionResult LinkItemCreate()
        {
            var model = new ImportantLinksModelDto();
            return View("Footer/LinkItemCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkItemCreate(ImportantLinksModelDto linkModel)
        {
            if (ModelState.IsValid)
            {
                if (linkModel != null)
                {
                    if (await _repo.CreateImportantLinkAsyncTask(linkModel))
                    {
                        HttpContext.Session.SetString("Success", "Link Created Successfully.");
                        return RedirectToAction(nameof(Footer));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(Footer));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Footer));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Footer));
            }
        }

        public async Task<IActionResult> LinkItemUpdate(int id)
        {
            var dataSet = await _repo.FetchImportantLinksByIdAsyncTask(id);
            return View("Footer/LinkItemUpdate", dataSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkItemUpdate(ImportantLinksModelDto linkModel)
        {
            if (ModelState.IsValid)
            {
                if (linkModel != null)
                {
                    if (await _repo.UpdateImportantLinkAsyncTask(linkModel))
                    {
                        HttpContext.Session.SetString("Success", "Link Updated Successfully.");
                        return RedirectToAction(nameof(Footer));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Footer));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(Footer));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Footer));
            }
        }

        [HttpPost]
        public async Task<IActionResult> LinkItemDelete(int id)
        {

            var dataSet = await _repo.FetchSalientFeaturesByIdAsyncTask(id);

            // Delete user
            if (await _repo.DeleteImportantLinkAsyncTask(dataSet.SalientFeatureId))
            {
                return Json("Success, Link deleted successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }

        #endregion

        #region Student Say

        public async Task<IActionResult> StudentsSay()
        {
            var combinedModel = new StudentSaysStudentsModelDto()
            {
                SayModel = await _repo.FetchStudentsSayAsyncTask(1),
                StudentsModel = await _repo.FetchStudentsSayingListAsyncTask()
            };
            return View("SayStudents/StudentsSay", combinedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentsSay(StudentSaysStudentsModelDto combinedModel)
        {
            if (ModelState.IsValid)
            {
                if (combinedModel.SayModel != null)
                {
                    // Update Image
                    var imageString = "";
                    var userDetails = await _repo.FetchStudentsSayAsyncTask(combinedModel.SayModel.StudentSayId);
                    if (combinedModel.SayModel.BackgroundImage != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\Students Say\Images\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(combinedModel.SayModel.BackgroundImage.FileName + Path.GetExtension(combinedModel.SayModel.BackgroundImage.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await combinedModel.SayModel.BackgroundImage.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(userDetails.Image))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(StudentsSay));
                        }
                        combinedModel.SayModel.Image = imageString;
                    }
                    else
                    {
                        combinedModel.SayModel.Image = userDetails.Image;
                    }


                    // Update in db
                    if (await _repo.UpdateStudentSayAsyncTask(combinedModel.SayModel))
                    {
                        HttpContext.Session.SetString("Success", "Student's Say Updated Successfully.");
                        return RedirectToAction(nameof(StudentsSay));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(StudentsSay));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(StudentsSay));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(StudentsSay));
            }
        }

        public IActionResult StudentSayCreate()
        {
            var model = new StudentSayStudentsModelDto();
            return View("SayStudents/StudentSayCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentSayCreate(StudentSayStudentsModelDto studentSay)
        {
            if (ModelState.IsValid)
            {
                if (studentSay != null)
                {
                    // Update image 
                    // Update Image
                    var imageString = "";
                    if (studentSay.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\Students Say\Images\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(studentSay.ImageString.FileName + Path.GetExtension(studentSay.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await studentSay.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                    }
                    if (string.IsNullOrWhiteSpace(imageString))
                    {
                        HttpContext.Session.SetString("Error", "Image is required!");
                        return RedirectToAction(nameof(StudentsSay));
                    }

                    studentSay.Image = imageString;

                    // Update db
                    if (await _repo.CreateStudentsSayingAsyncTask(studentSay))
                    {
                        HttpContext.Session.SetString("Success", "Student saying created Successfully.");
                        return RedirectToAction(nameof(StudentsSay));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while adding the data!");
                        return RedirectToAction(nameof(StudentsSay));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(StudentsSay));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(StudentsSay));
            }
        }

        public async Task<IActionResult> StudentSayUpdate(int id)
        {
            var dataSet = await _repo.FetchStudentsSayingByIdAsyncTask(id);
            return View("SayStudents/StudentSayUpdate", dataSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentSayUpdate(StudentSayStudentsModelDto sayStudents)
        {
            if (ModelState.IsValid)
            {
                if (sayStudents != null)
                {
                    // Update image 
                    var imageString = "";
                    var userDetails = await _repo.FetchStudentsSayingByIdAsyncTask(sayStudents.StudentSayId);
                    if (sayStudents.ImageString != null)
                    {
                        // Users Folder
                        var userImagePath = @"\User_Information\Pages\Students Say\Images\";
                        // Root Path
                        var webRootPath = _env.WebRootPath;
                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        // File
                        var fileName = Path.GetFileNameWithoutExtension(sayStudents.ImageString.FileName + Path.GetExtension(sayStudents.ImageString.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await sayStudents.ImageString.CopyToAsync(stream);
                        }
                        imageString = userImagePath + fileName;
                        if (string.IsNullOrWhiteSpace(imageString) && string.IsNullOrWhiteSpace(userDetails.Image))
                        {
                            HttpContext.Session.SetString("Error", "Image is required!");
                            return RedirectToAction(nameof(StudentsSay));
                        }
                        sayStudents.Image = imageString;
                    }
                    else
                    {
                        sayStudents.Image = userDetails.Image;
                    }

                    // Update database
                    if (await _repo.UpdateStudentsSayingAsyncTask(sayStudents))
                    {
                        HttpContext.Session.SetString("Success", "Students saying updated Successfully.");
                        return RedirectToAction(nameof(StudentsSay));
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(StudentsSay));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return RedirectToAction(nameof(StudentsSay));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(StudentsSay));
            }
        }

        [HttpPost]
        public async Task<IActionResult> StudentSayDelete(int id)
        {

            var dataSet = await _repo.FetchStudentsSayingByIdAsyncTask(id);

            var userImagePath = dataSet.Image;
            // Base Path

            // Delete User image folder
            // Root Path
            var webRootPath = _env.WebRootPath;
            // Base Path
            var basePath = Path.Combine(webRootPath + userImagePath);
            // Base Path Exists or create new base path
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (basePathExists) System.IO.File.Delete(basePath);

            // Delete user
            if (await _repo.DeleteStudentsSayingAsyncTask(dataSet.StudentSayId))
            {
                return Json("Success, Students saying successfully");
            }
            else
            {
                return Json("Error Problem Deleting User");
            }
        }

        #endregion
    }
}
