using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using College.Model.DataTransferObject.EmailExtraDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace College.Controllers
{
    public class VerificationController : Controller
    {
        private readonly IEmailRepo _email;
        private readonly IAuthRepo _auth;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public VerificationController(IEmailRepo email, IAuthRepo auth, IWebHostEnvironment hostingEnvironment)
        {
            this._email = email;
            this._auth = auth;
            this._hostingEnvironment = hostingEnvironment;
        }
        [AuthOverride]
        [HttpGet]
        public async Task<IActionResult> SendVerificationEmail()
        {
            string companyName = "SBDM Polytechnic Institute";
            string baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            var emailConfigDbDetails = await _email.FetchEmailByFilter(1);

            var emailVerification = await _auth.VerifyEmailRequestAsyncTask(userDetails.Email);
            var verificationUrl = baseUrl + "/Verification/VerifyEmail?Email=" + emailVerification.Email + "&Token=" + emailVerification.Token;

            var resetEmailObject = new ButtonedEmailModelDto()
            {
                // Html Title 
                Title = "Email Verification",
                // Html Fav Icon
                FavIconLink = " http://bishnudhani.edu.np/wwwroot/favicon_io_Dark/favicon.ico",
                // Html Apple touch Icon
                AppleTouchIconLink = "http://bishnudhani.edu.np/wwwroot/favicon_io_Dark/apple-touch-icon.png",
                // Company Logo
                LogoImageLink = "http://bishnudhani.edu.np/wwwroot/favicon_io_Dark/android-chrome-512x512.png",
                // Email Purpose
                Purpose = "Email Verification",
                // Email User Name
                UserName = userDetails.FullName,
                // Email Message
                Message = "Please click the link below to verify your email.",
                // Click Button Url
                ButtonUrl = verificationUrl,
                // Click Button Text
                ButtonText = "Click Me.",
                // Click Button Warning Message
                BoodyWarningMessage = "This link is will redirect you to verification portal.",
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
            EmailController email = new EmailController(_hostingEnvironment);
            var response = await email.SendEmail(null, resetEmailObject, emailConfigDbDetails);
            if (response)
            {
                HttpContext.Session.SetString("Success",
                    "Verification email has been sent to " + userDetails.Email);
                return RedirectToAction("Logout", "Login");
            }
            else
            {
                HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                return RedirectToAction("Index", "ControlPanel");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string Email, string Token)
        {
            // Fetch Details Fo email is valid or Not
            var verificationModel = new EmailVerificationModelDto()
            {
                Email = Email,
                Token = Token
            };
            var result = await _auth.EmailVerifiedAsyncTask(verificationModel);
            if (result != null && result.Email == Email)
            {
                //Verify Email and Logout with session message
                HttpContext.Session.SetString("Success", "Email Verified Successfully " + Email);
                return RedirectToAction("Logout", "Login");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }
    }
}
