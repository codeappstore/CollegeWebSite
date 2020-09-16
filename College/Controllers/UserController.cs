using CodeAppStore.License.RandomStringRepo;
using College.Access.IRepository;
using College.Database.Helper;
using College.Helpers;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using College.Model.DataTransferObject.EmailExtraDto;
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
        private readonly IAuthRepo _auth;
        private readonly IEmailRepo _email;
        private readonly IWebHostEnvironment _env;
        readonly IRandomString _random = new RandomString();

        public UserController(IAuthRepo _auth, IWebHostEnvironment _env, IEmailRepo _email)
        {
            this._auth = _auth;
            this._env = _env;
            this._email = _email;
        }
        public async Task<ActionResult> Index()
        {
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            var result = await _auth.FetchUsersListAsyncTask(userDetails.AuthId);
            return View(result);
        }
        public async Task<ActionResult> Details(int id)
        {
            var userDetails = await _auth.FetchUserByFilter(null, id);
            if (userDetails != null) return View(userDetails);

            HttpContext.Session.SetString("Error", "User Not Found or Problem connecting gto server !");
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            var newModel = new AuthImageModelDto();
            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthImageModelDto authImage)
        {
            var builder = new RandomStringBuilder();
            if (ModelState.IsValid)
            {
                if (authImage.AuthModel != null || authImage.ImageModel != null)
                    try
                    {
                        if (await _auth.IsUniqueUserCredentials(authImage.AuthModel.Email, authImage.AuthModel.UserName,
                            authImage.AuthModel.PhoneNumber))
                        {
                            var imageString = "";
                            if (authImage.ImageModel != null)
                            {
                                var userImagePath =
                                    @"\User_Information\Users\" + authImage.AuthModel.Email + @"\Images\";
                                var webRootPath = _env.WebRootPath;
                                var basePath = Path.Combine(webRootPath + userImagePath);
                                var basePathExists = Directory.Exists(basePath);
                                if (!basePathExists) Directory.CreateDirectory(basePath);
                                var fileName = Path.GetFileNameWithoutExtension(
                                    authImage.ImageModel.Image.FileName +
                                    Path.GetExtension(authImage.ImageModel.Image.FileName));
                                var filePath = Path.Combine(basePath, fileName);
                                var fileExists = System.IO.File.Exists(filePath);
                                if (fileExists) System.IO.File.Delete(filePath);
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await authImage.ImageModel.Image.CopyToAsync(stream);
                                }

                                imageString = userImagePath + fileName;
                            }

                            _ = !string.IsNullOrWhiteSpace(imageString)
                                ? authImage.AuthModel.Image = imageString
                                : authImage.AuthModel.Image = null;

                            authImage.AuthModel.Password =
                                _random.RandomStringGenerator((int)RandomStringBuilder.PurposeOfString.PASSWORD);
                            authImage.AuthModel.Allowed = IsAllowed.Enabled;
                            if (await _auth.CreateNewUserAsyncTask(authImage.AuthModel))
                            {
                                var eModelDto = new ResetRequestModelDto
                                {
                                    Email = authImage.AuthModel.Email
                                };

                                var companyName = "SBDM Polytechnic Institute";
                                var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                                var userDetails = await _auth.BasicUserDetailsAsyncTask(eModelDto.Email);

                                var resetData = await _auth.ResetRequestAsyncTask(eModelDto.Email);

                                var emailConfigDbDetails = await _email.FetchEmailByFilter(1);

                                var verificationUrl = baseUrl + "/Reset/RequestApproved?Email=" + resetData.Email +
                                                      "&Token=" + resetData.Token + "&ExpirationDateTime=" +
                                                      resetData.ExpirationDateTime + "&IssuedDateTime=" +
                                                      resetData.IssuedDateTime;

                                var resetEmailObject = new ButtonedEmailModelDto
                                {
                                    Title = "First time password reset",
                                    FavIconLink = " http://bishnudhani.edu.np/favicon_io_Dark/favicon.ico",
                                    AppleTouchIconLink =
                                        "http://bishnudhani.edu.np/favicon_io_Dark/apple-touch-icon.png",
                                    LogoImageLink =
                                        "http://bishnudhani.edu.np/favicon_io_Dark/android-chrome-512x512.png",
                                    Purpose = "First time password reset",
                                    UserName = userDetails.FullName,
                                    Message = "Please click the link below to reset password.",
                                    ButtonUrl = verificationUrl,
                                    ButtonText = "Click Me.",
                                    BoodyWarningMessage =
                                        "This link is valid of 20 min only. This link is will redirect you to reset portal.",
                                    Company = companyName,
                                    CopyRightYear = DateTime.UtcNow.Year.ToString(),
                                    FooterCompany = companyName,
                                    CompanyLink = "http://bishnudhani.edu.np",
                                    CompanyLinkText = companyName,
                                    Warning =
                                        "If you are not associated with this organization please ignore this email!",
                                    Description =
                                        "Shahid Bishnu Dhani Memorial is a newly established educational institution which primarily attempts to contribute to the development of the country through the production of skilled and semi-skilled human resources. ",
                                    HomeText = "Home",
                                    HomeLink = "http://bishnudhani.edu.np",
                                    ContactText = "Forestry",
                                    ContactLink = "http://bishnudhani.edu.np/Home/Forestry",
                                    ServiceText = "Agriculture",
                                    ServiceLink = "http://bishnudhani.edu.np/Home/Agriculture",
                                    DisplayName = emailConfigDbDetails.From,
                                    To = userDetails.Email,
                                    Subject = "Dear, " + userDetails.FullName +
                                              " complete the process to verify your email, Shahid Bishnu Dhani Memorial Polytechnic Institute"
                                };
                                var email = new EmailController(_env, _email);
                                var response = await email.SendEmail(null, resetEmailObject, emailConfigDbDetails);
                                if (response)
                                {
                                    HttpContext.Session.SetString("Success",
                                        "User " + authImage.AuthModel.FullName +
                                        " Added Successfully, Reset password email has been sent ");
                                    return RedirectToAction(nameof(Index));
                                }

                                HttpContext.Session.SetString("Error",
                                    "Problem Connecting to server, Please Try Again later!");
                                return RedirectToAction(nameof(Index));
                            }

                            HttpContext.Session.SetString("Error", "Problem connecting To server!");
                            return View();
                        }

                        HttpContext.Session.SetString("Error", "Email, User Name or Phone Number must be unique !");
                        return View();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        HttpContext.Session.SetString("Error", "Problem connecting To server or Email!");
                        return View();
                    }

                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return View();
            }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var userDetails = await _auth.FetchUserByFilter(null, id);
            if (userDetails != null) return View(userDetails);

            HttpContext.Session.SetString("Error", "User Not Found or Problem connecting gto server !");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AuthUpdateModelDto authUpdate)
        {
            if (ModelState.IsValid)
                try
                {
                    var authData = await _auth.FetchUserByFilter(null, authUpdate.AuthId);
                    authUpdate.Email = authData.Email;
                    authUpdate.Password = authData.Password;
                    authUpdate.IsEmailVerified = authData.IsEmailVerified;
                    authUpdate.DateEmailVerified = authData.DateEmailVerified;
                    authUpdate.Allowed = authData.Allowed;
                    authUpdate.Image = authData.Image;

                    if (await _auth.UpdateExistingUserAsyncTask(authUpdate))
                    {
                        HttpContext.Session.SetString("Success",
                            "User " + authUpdate.FullName + " Updated Successfully!! ");
                        return RedirectToAction(nameof(Index));
                    }

                    HttpContext.Session.SetString("Error", "Problem connecting To server!");
                    return View();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    HttpContext.Session.SetString("Error", "Problem connecting To server or Email!");
                    return View();
                }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int UserID, string FullName)
        {
            if (string.IsNullOrWhiteSpace(FullName)) return Json("Error User requested is not found");
            if (!await _auth.UserExistsAsyncTask(UserID)) return Json("Error User requested is not found");

            var userData = await _auth.FetchUserByFilter(null, UserID);

            if (await _auth.DeleteUserAsyncTask(userData.AuthId))
            {
                var userFolderPath = @"\User_Information\Users\" + userData.Email;
                var webRootPath = _env.WebRootPath;
                var basePath = Path.Combine(webRootPath + userFolderPath);
                var basePathExists = Directory.Exists(basePath);
                if (basePathExists) Directory.Delete(basePath, true);
                return Json("Success, " + userData.FullName + "'s all data deleted successfully");
            }

            return Json("Error Problem Deleting User");
        }
    }
}