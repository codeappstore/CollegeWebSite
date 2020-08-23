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
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IAuthRepo _auth;
        private readonly IEmailRepo _email;
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

                string companyName = "SBDM Polytechnic Institute";
                string baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                var userDetails = await _auth.BasicUserDetailsAsyncTask(eModelDto.Email);

                var resetData = await _auth.ResetRequestAsyncTask(eModelDto.Email);

                var emailConfigDbDetails = await _email.FetchEmailByFilter(1);

                var verificationUrl = baseUrl + "/Reset/RequestApproved?Email=" + resetData.Email + "&Token=" + resetData.Token + "&ExpirationDateTime=" + resetData.ExpirationDateTime + "&IssuedDateTime=" + resetData.IssuedDateTime;

                var resetEmailObject = new ButtonedEmailModelDto()
                {
                    // Html Title 
                    Title = "Password Reset",
                    // Html Fav Icon
                    FavIconLink = " http://bishnudhani.edu.np/wwwroot/favicon_io_Dark/favicon.ico",
                    // Html Apple touch Icon
                    AppleTouchIconLink = "http://bishnudhani.edu.np/wwwroot/favicon_io_Dark/apple-touch-icon.png",
                    // Company Logo
                    LogoImageLink = "http://bishnudhani.edu.np/wwwroot/favicon_io_Dark/android-chrome-512x512.png",
                    // Email Purpose
                    Purpose = "Password Reset",
                    // Email User Name
                    UserName = userDetails.FullName,
                    // Email Message
                    Message = "Please click the link below to reset password.",
                    // Click Button Url
                    ButtonUrl = verificationUrl,
                    // Click Button Text
                    ButtonText = "Click Me.",
                    // Click Button Warning Message
                    BoodyWarningMessage = "This link is valid of 20 min only. This link is will redirect you to reset portal.",
                    // Sincerely Company
                    Company = companyName,
                    // Copyright Year
                    CopyRightYear = DateTime.UtcNow.Year.ToString(),
                    //Footer Company Name Before Copyright
                    FooterCompany = companyName,
                    // Know More Link
                    CompanyLink = "http://bishnudhani.edu.np",
                    // Know More About Company Name
                    CompanyLinkText = companyName,
                    // Unverified Warning
                    Warning = "If you didn't request for this service please ignore this email!",
                    // Company Slogan Or description
                    Description = "Shahid Bishnu Dhani Memorial is a newly established educational institution which primarily attempts to contribute to the development of the country through the production of skilled and semi-skilled human resources. ",

                    // Home Button and Link
                    HomeText = "Home",
                    HomeLink = "http://bishnudhani.edu.np",

                    // Contact Button and Link
                    ContactText = "Forestry",
                    ContactLink = "http://bishnudhani.edu.np/Home/Forestry",

                    // Service Button and Link
                    ServiceText = "Agriculture",
                    ServiceLink = "http://bishnudhani.edu.np/Home/Agriculture",

                    //Email Sender receiver configuration
                    DisplayName = emailConfigDbDetails.From,
                    To = userDetails.Email,
                    Subject = "Dear, " + userDetails.FullName + " complete the process to verify your email, Shahid Bishnu Dhani Memorial Polytechnic Institute"
                };

                // Send Email And show verification send email redirecting to index 
                EmailController email = new EmailController(_hostingEnvironment, _email);
                var response = await email.SendEmail(null, resetEmailObject, emailConfigDbDetails);
                if (response)
                {
                    HttpContext.Session.SetString("Success",
                        "Reset email has been sent to " + userDetails.Email);
                    return RedirectToAction("Logout", "Login");
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                    return RedirectToAction("Index", "ControlPanel");
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction("ForgetPassword", "Reset");
            }

        }

        public async Task<IActionResult> RequestApproved(string Email, string Token, DateTime ExpirationDateTime, DateTime IssuedDateTime)
        {
            var responseModel = new ResetResponseModelDto()
            {
                Email = Email,
                Token = Token,
                ExpirationDateTime = ExpirationDateTime,
                IssuedDateTime = IssuedDateTime
            };
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Token))
            {
                HttpContext.Session.SetString("Error", "Invalid Token!");
                return RedirectToAction("ForgetPassword", "Reset");
            }

            if (ExpirationDateTime < DateTime.Now)
            {
                HttpContext.Session.SetString("Error", "Token has Expired!");
                return RedirectToAction("ForgetPassword", "Reset");
            }

            if (!await _auth.ResetInformationValid(responseModel))
            {
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
                else
                {
                    HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                    HttpContext.Session.SetString("_Reset", resetPassword.Email);
                    return RedirectToAction("ResetPassword", "Reset");
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                HttpContext.Session.SetString("_Reset", resetPassword.Email);
                return RedirectToAction("ResetPassword", "Reset");
            }
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
