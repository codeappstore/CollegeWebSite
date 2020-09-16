using College.Access.IRepository;
using College.Model.DataTransferObject.AuthExtraDto;
using College.Model.DataTransferObject.EmailExtraDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace College.Controllers
{
    public class ResetController : Controller
    {
        private readonly IAuthRepo _auth;
        private readonly IEmailRepo _email;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ResetController(IAuthRepo _auth, IWebHostEnvironment _hostingEnvironment, IEmailRepo _email)
        {
            this._auth = _auth;
            this._email = _email;
            this._hostingEnvironment = _hostingEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult> ResetRequest(ResetRequestModelDto eModelDto)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(eModelDto.Email))
                {
                    HttpContext.Session.SetString("Warning", "Email is invalid!");
                    return RedirectToAction("ForgetPassword", "Reset");
                }

                if (!await _auth.UserExistsAsyncTask(null, eModelDto.Email))
                {
                    HttpContext.Session.SetString("Warning", "User does not exists!");
                    return RedirectToAction("ForgetPassword", "Reset");
                }

                var companyName = "SBDM Polytechnic Institute";
                var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                var userDetails = await _auth.BasicUserDetailsAsyncTask(eModelDto.Email);

                var resetData = await _auth.ResetRequestAsyncTask(eModelDto.Email);

                var emailConfigDbDetails = await _email.FetchEmailByFilter(1);

                var verificationUrl = baseUrl + "/Reset/RequestApproved?Email=" + resetData.Email + "&Token=" +
                                      resetData.Token + "&ExpirationDateTime=" + resetData.ExpirationDateTime +
                                      "&IssuedDateTime=" + resetData.IssuedDateTime;

                var resetEmailObject = new ButtonedEmailModelDto
                {
                    Title = "Password Reset",
                    FavIconLink = " http://bishnudhani.edu.np/favicon_io_Dark/favicon.ico",
                    AppleTouchIconLink = "http://bishnudhani.edu.np/favicon_io_Dark/apple-touch-icon.png",
                    LogoImageLink = "http://bishnudhani.edu.np/favicon_io_Dark/android-chrome-512x512.png",
                    Purpose = "Password Reset",
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
                    Warning = "If you didn't request for this service please ignore this email!",
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

                var email = new EmailController(_hostingEnvironment, _email);
                var response = await email.SendEmail(null, resetEmailObject, emailConfigDbDetails);
                if (response)
                {
                    HttpContext.Session.SetString("Success",
                        "Reset email has been sent to " + userDetails.Email);
                    return RedirectToAction("Logout", "Login");
                }

                HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                return RedirectToAction("Index", "ControlPanel");
            }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return RedirectToAction("ForgetPassword", "Reset");
        }

        public async Task<IActionResult> RequestApproved(string Email, string Token, DateTime ExpirationDateTime,
            DateTime IssuedDateTime)
        {
            var responseModel = new ResetResponseModelDto
            {
                Email = Email,
                Token = Token,
                ExpirationDateTime = ExpirationDateTime,
                IssuedDateTime = IssuedDateTime
            };
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Token))
            {
                await _auth.RemoveInvalidResetRequest(responseModel);
                HttpContext.Session.SetString("Error", "Invalid Token!");
                return RedirectToAction("ForgetPassword", "Reset");
            }

            if (ExpirationDateTime < DateTime.Now)
            {
                await _auth.RemoveInvalidResetRequest(responseModel);
                HttpContext.Session.SetString("Error", "Token has Expired!");
                return RedirectToAction("ForgetPassword", "Reset");
            }

            if (!await _auth.ResetInformationValid(responseModel))
            {
                await _auth.RemoveInvalidResetRequest(responseModel);
                HttpContext.Session.SetString("Error", "Invalid Token!");
                return RedirectToAction("ForgetPassword", "Reset");
            }

            HttpContext.Session.SetString("Success", "Enter new Password!");
            HttpContext.Session.SetString("_Reset", Email);
            return RedirectToAction("ResetPassword", "Reset");
        }


        [HttpPost]
        public async Task<IActionResult> ResetPasswordResponse(ResetPasswordModelDto resetPassword)
        {
            if (ModelState.IsValid)
            {
                var result = await _auth.ResetApprovedAsyncTask(resetPassword);
                if (result.Email == resetPassword.Email)
                {
                    HttpContext.Session.SetString("Success",
                        "Password Reset Successfully " + resetPassword.Email);
                    return RedirectToAction("Logout", "Login");
                }

                HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                HttpContext.Session.SetString("_Reset", resetPassword.Email);
                return RedirectToAction("ResetPassword", "Reset");
            }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            HttpContext.Session.SetString("_Reset", resetPassword.Email);
            return RedirectToAction("ResetPassword", "Reset");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}