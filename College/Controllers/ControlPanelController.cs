﻿using CodeAppStore.License.LicenseRepo;
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
        private readonly IAuthRepo _auth;
        private readonly IWebHostEnvironment _env;
        private readonly ILicense _license = new License();

        public ControlPanelController(IAuthRepo _auth, IWebHostEnvironment env)
        {
            this._auth = _auth;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var countMode = new CountModelDto
            {
                Administrator = _auth.AdminCount(),
                Developer = _auth.DeveloperCount(),
                Manager = _auth.ManagerCount(),
                Users = _auth.UserCount()
            };
            var authLicense = await _auth.FetchSettings();
            var license = _license.CheckLicenseVerification(authLicense.License, authLicense.Certificate,
                authLicense.ClientCode,
                authLicense.ProjectCode);
            HttpContext.Session.SetString("_License", license.Expiry);
            return View(countMode);
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult SettingsResetRequest()
        {
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            HttpContext.Session.SetString("_Reset", userDetails.Email);
            return RedirectToAction("ResetPassword", "Reset");
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
                        var userSessionDetails =
                            HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
                        var userImagePath = @"\User_Information\Users\" + userSessionDetails.Email + @"\Images\";
                        var webRootPath = _env.WebRootPath;
                        var basePath = Path.Combine(webRootPath + userImagePath);
                        var basePathExists = Directory.Exists(basePath);
                        if (!basePathExists) Directory.CreateDirectory(basePath);
                        var fileName =
                            Path.GetFileNameWithoutExtension(
                                imageModel.Image.FileName + Path.GetExtension(imageModel.Image.FileName));
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
                            HttpContext.Session.SetString("Success",
                                "Image  Updated Successfully " + userDetails.FullName);
                            return RedirectToAction("Account", "ControlPanel");
                        }

                        HttpContext.Session.SetString("Error", "Problem connecting To server!");
                        return RedirectToAction("Account", "ControlPanel");
                    }

                    HttpContext.Session.SetString("Error", "Image length is invalids!");
                    return RedirectToAction("Account", "ControlPanel");
                }

                HttpContext.Session.SetString("Error", "Problem connecting To server!");
                return RedirectToAction(nameof(Account));
            }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return RedirectToAction(nameof(Account));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccountInfo(AuthUpdateModelDto auth)
        {
            if (ModelState.IsValid)
                try
                {
                    var completeUserInfo = await _auth.FetchUserByFilter(null, auth.AuthId);
                    auth.Email = completeUserInfo.Email;
                    auth.Password = completeUserInfo.Password;
                    auth.Image = completeUserInfo.Image;
                    auth.IsEmailVerified = completeUserInfo.IsEmailVerified;
                    auth.DateEmailVerified = completeUserInfo.DateEmailVerified;
                    auth.Allowed = completeUserInfo.Allowed;
                    auth.RoleId = completeUserInfo.RoleId;
                    if (await _auth.UpdateExistingUserAsyncTask(auth))
                    {
                        var userSessionDetails =
                            HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
                        var userDetails = await _auth.BasicUserDetailsAsyncTask(userSessionDetails.Email);
                        HttpContext.Session.SetComplexData("_Details", userDetails);
                        HttpContext.Session.SetString("Success",
                            "Account Information Updated Successfully " + userDetails.FullName);
                        return RedirectToAction("Account", "ControlPanel");
                    }

                    HttpContext.Session.SetString("Error", "Problem while updating the data!");
                    return RedirectToAction(nameof(Account));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    HttpContext.Session.SetString("Error", "Problem connecting To server!");
                    return RedirectToAction(nameof(Account));
                }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return RedirectToAction(nameof(Account));
        }

        public async Task<IActionResult> Account()
        {
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            if (userDetails != null)
            {
                var authEditDetails = await _auth.FetchUserByFilter(userDetails.Email);
                return View(authEditDetails);
            }

            HttpContext.Session.SetString("Warning", "Session Expired, Please Login First!");
            return RedirectToAction("Logout", "Login");
        }
    }
}