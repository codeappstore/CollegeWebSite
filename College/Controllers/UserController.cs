using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace College.Controllers
{
    [AuthOverride]
    public class UserController : Controller
    {
        private IAuthRepo _auth;
        private IWebHostEnvironment _env;
        public UserController(IAuthRepo _auth, IWebHostEnvironment _env)
        {
            this._auth = _auth;
            this._env = _env;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            var result = await _auth.FetchUsersListAsyncTask(userDetails.AuthId);
            return View(result);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            var newModel = new AuthImageModelDto();


            return View(newModel);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthImageModelDto authImage)
        {
            if (ModelState.IsValid)
            {
                if (authImage.AuthModel != null || authImage.ImageModel != null)
                {
                    try
                    {
                        if (await _auth.IsUniqueUserCredentials(authImage.AuthModel.Email, authImage.AuthModel.UserName, authImage.AuthModel.PhoneNumber))
                        {
                            var imageString = "";
                            if (authImage.ImageModel.Image != null && authImage.ImageModel.Image.Length > 0)
                            {
                                // Users Folder
                                var userImagePath = @"\User_Information\" + authImage.AuthModel.Email + @"\Images\";
                                // Root Path
                                var webRootPath = _env.WebRootPath;
                                // Base Path
                                var basePath = Path.Combine(webRootPath + userImagePath);
                                // Base Path Exists or create new base path
                                bool basePathExists = System.IO.Directory.Exists(basePath);
                                if (!basePathExists) Directory.CreateDirectory(basePath);
                                // File
                                var fileName = Path.GetFileNameWithoutExtension(authImage.ImageModel.Image.FileName + Path.GetExtension(authImage.ImageModel.Image.FileName));
                                var filePath = Path.Combine(basePath, fileName);
                                var fileExists = System.IO.File.Exists(filePath);
                                if (fileExists) System.IO.File.Delete(filePath);
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await authImage.ImageModel.Image.CopyToAsync(stream);
                                }
                                imageString = userImagePath + fileName;
                            }
                            _ = !string.IsNullOrWhiteSpace(imageString) ? authImage.AuthModel.Image = imageString : authImage.AuthModel.Image = null;
                            authImage.AuthModel.Password = "DemoUserReset";
                            authImage.AuthModel.Allowed = IsAllowed.Enabled;

                            if (await _auth.CreateNewUserAsyncTask(authImage.AuthModel))
                            {
                                HttpContext.Session.SetString("Success", "User " + authImage.AuthModel.FullName + " Added Successfully, Reset password on first login ");
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                HttpContext.Session.SetString("Error", "Problem connecting To server!");
                                return View();
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("Error", "Email, User Name or Phone Number must be unique !");
                            return View();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        HttpContext.Session.SetString("Error", "Problem connecting To server or Email!");
                        return View();
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                    return View();
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
