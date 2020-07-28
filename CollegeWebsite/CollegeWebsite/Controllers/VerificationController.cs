using CollegeWebsite.DataAccess.Models.Emails.Dtos;
using CollegeWebsite.DataAccess.Models.Emails.Services.IRepo;
using CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos;
using CollegeWebsite.Override;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CollegeWebsite.Controllers
{

    public class VerificationController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IEmailConfigRepo _emailConfig;

        public VerificationController(ILogger<LoginController> logger, IEmailConfigRepo _emailConfig)
        {
            _logger = logger;
            this._emailConfig = _emailConfig;
        }
        [AuthOverride]
        [HttpGet]
        public async Task<IActionResult> SendVerificationEmail()
        {
            string companyName = "SBDM Polytechnic Institute";
            string baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            var detailsToken = HttpContext.Session.GetComplexData<BasicDetailsDto>("_Details");
            var emailConfigDetails = _emailConfig.GetEmailConfiguration();
            var verificationUrl = baseUrl + "/Verification/VerifyEmail?email=" + detailsToken.Email;
            var emailVerificationUrl = emailConfigDetails.BaseUrl + "/api/Email/ButtenedEmail";
            var resetEmailObject = new EmailSenderButtonedDto
            {
                // Html Title 
                Title = "Email Verification",
                // Html Fav Icon
                FavIconLink = "http://bishnudhani.edu.np/favicon_io_Dark/favicon.ico",
                // Html Apple touch Icon
                AppleTouchIconLink = "http://bishnudhani.edu.np/favicon_io_Dark/apple-touch-icon.png",
                // Company Logo
                LogoImageLink = "http://bishnudhani.edu.np/favicon_io_Dark/android-chrome-512x512.png",
                // Email Purpose
                Purpose = "Email Verification",
                // Email User Name
                UserName = detailsToken.FullName,
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

                // Email Server Configuration
                MailServer = emailConfigDetails.MailServer,
                Password = emailConfigDetails.Password,
                SmtpPort = emailConfigDetails.SmtpPort,

                //Email Sender receiver configuration
                DisplayName = emailConfigDetails.Form,
                From = emailConfigDetails.UserName,
                To = detailsToken.Email,
                Subject = "Dear, " + detailsToken.FullName + " complete the process to verify your email, Shahid Bishnu Dhani Memorial Polytechnic Institute"
            };

            // Send Email And show verification send email redirecting to index 
            var client = new HttpClient();
            var jsonObject = JsonConvert.SerializeObject(resetEmailObject);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(emailVerificationUrl, content);

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("Success",
                        "Verification email has been sent to " + detailsToken.Email);
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
                HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                return RedirectToAction("Index", "ControlPanel");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            // Fetch Details Fo email is valid or Not

            var emailConfigDetails = _emailConfig.GetEmailConfiguration();
            var verifyUrl = emailConfigDetails.BaseUrl + "/api/VerificationAsync/" + email;
            var client = new HttpClient();

            var verifyDetails = await client.GetAsync(verifyUrl);

            if (verifyDetails.IsSuccessStatusCode)
            {

                //Verify Email and Logout with session message
                HttpContext.Session.SetString("Success", "Email Verified Successfully " + email);
                return RedirectToAction("Logout", "Login");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }


    }
}
