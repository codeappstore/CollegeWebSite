using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace College.Controllers
{
    [AuthOverride]
    public class ControlPanelController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IAuthRepo _auth;
        public ControlPanelController(IAuthRepo _auth, IWebHostEnvironment env)
        {
            this._auth = _auth;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountImage(ImageModelDto imageModel)
        {
            if (ModelState.IsValid)
            {
                if (imageModel.Image != null)
                {
                    if (imageModel.Image.Length > 0)
                    {
                        // User Details From Session
                        var userSessionDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");

                        // Users Folder
                        var userImagePath = @"\User_Information\" + userSessionDetails.Email + @"\Images\";

                        // Root Path
                        var webRootPath = _env.WebRootPath;

                        // Base Path
                        var basePath = Path.Combine(webRootPath + userImagePath);

                        // Base Path Exists or create new base path
                        bool basePathExists = System.IO.Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);

                        // File
                        var fileName = Path.GetFileNameWithoutExtension(imageModel.Image.FileName + Path.GetExtension(imageModel.Image.FileName));
                        var filePath = Path.Combine(basePath, fileName);
                        var fileExists = System.IO.File.Exists(filePath);
                        if (fileExists) System.IO.File.Delete(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageModel.Image.CopyToAsync(stream);
                        }
                        var completeUserInfo = await _auth.FetchUserByFilter(null, userSessionDetails.AuthId);
                        completeUserInfo.Image = userImagePath + fileName;
                        if (await _auth.UpdateExistingUserAsyncTask(completeUserInfo))
                        {
                            var userDetails = await _auth.BasicUserDetailsAsyncTask(userSessionDetails.Email);
                            HttpContext.Session.SetComplexData("_Details", userDetails);
                            HttpContext.Session.SetString("Success", "Image  Updated Successfully " + userDetails.FullName);
                            return RedirectToAction("Account", "ControlPanel");
                        }
                        else
                        {
                            HttpContext.Session.SetString("Error", "Problem connecting To server!");
                            return RedirectToAction("Account", "ControlPanel");
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Image length is invalids!");
                        return RedirectToAction("Account", "ControlPanel");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Problem connecting To server!");
                    return RedirectToAction(nameof(Account));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Account));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountInfo(AuthUpdateModelDto auth)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var completeUserInfo = await _auth.FetchUserByFilter(null, auth.AuthId);
                    auth.Email = completeUserInfo.Email;
                    auth.Password = completeUserInfo.Password;
                    if (await _auth.UpdateExistingUserAsyncTask(auth))
                    {
                        var userSessionDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
                        var userDetails = await _auth.BasicUserDetailsAsyncTask(userSessionDetails.Email);
                        HttpContext.Session.SetComplexData("_Details", userDetails);
                        HttpContext.Session.SetString("Success", "Account Information Updated Successfully " + userDetails.FullName);
                        return RedirectToAction("Account", "ControlPanel");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Error", "Problem while updating the data!");
                        return RedirectToAction(nameof(Account));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    HttpContext.Session.SetString("Error", "Problem connecting To server!");
                    return RedirectToAction(nameof(Account));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Account));
            }
        }
        public async Task<IActionResult> Account()
        {
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");

            if (userDetails != null)
            {
                var authEditDetails = await _auth.FetchUserByFilter(userDetails.Email);
                return View(authEditDetails);
            }
            else
            {
                HttpContext.Session.SetString("Warning", "Session Expired, Please Login First!");
                return RedirectToAction("Logout", "Login");
            }
        }
    }
}
