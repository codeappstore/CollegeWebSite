using System;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using College.Model.DataTransferObject.EmailExtraDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Controllers
{
    public class VerificationController : Controller
    {
        private readonly IAuthRepo _auth;
        private readonly IEmailRepo _email;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public VerificationController(IEmailRepo email, IAuthRepo auth, IWebHostEnvironment hostingEnvironment)
        {
            _email = email;
            _auth = auth;
            _hostingEnvironment = hostingEnvironment;
        }

        [AuthOverride]
        [HttpGet]
        public async Task<IActionResult> SendVerificationEmail()
        {
            var companyName = "SBDM Polytechnic Institute";
            var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            var emailConfigDbDetails = await _email.FetchEmailByFilter(1);

            var emailVerification = await _auth.VerifyEmailRequestAsyncTask(userDetails.Email);
            var verificationUrl = baseUrl + "/Verification/VerifyEmail?Email=" + emailVerification.Email + "&Token=" +
                                  emailVerification.Token;

            var resetEmailObject = new ButtonedEmailModelDto
            {
                Title = "Email Verification",
                FavIconLink = " http://bishnudhani.edu.np/favicon_io_Dark/favicon.ico",
                AppleTouchIconLink = "http://bishnudhani.edu.np/favicon_io_Dark/apple-touch-icon.png",
                LogoImageLink = "http://bishnudhani.edu.np/favicon_io_Dark/android-chrome-512x512.png",
                Purpose = "Email Verification",
                UserName = userDetails.FullName,
                Message = "Please click the link below to verify your email.",
                ButtonUrl = verificationUrl,
                ButtonText = "Click Me.",
                BoodyWarningMessage = "This link is will redirect you to verification portal.",
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
                    "Verification email has been sent to " + userDetails.Email);
                return RedirectToAction("Logout", "Login");
            }

            HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
            return RedirectToAction("Index", "ControlPanel");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string Email, string Token)
        {
            var verificationModel = new EmailVerificationModelDto
            {
                Email = Email,
                Token = Token
            };
            var result = await _auth.EmailVerifiedAsyncTask(verificationModel);
            if (result != null && result.Email == Email)
            {
                HttpContext.Session.SetString("Success", "Email Verified Successfully " + Email);
                return RedirectToAction("Logout", "Login");
            }

            return RedirectToAction("Logout", "Login");
        }
    }
}