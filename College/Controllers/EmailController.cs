using CodeAppStore.License.EncodeDecodeRepo;
using College.Access.IRepository;
using College.Database.Helper;
using College.Helpers;
using College.Model.DataTransferObject.EmailDto;
using College.Model.DataTransferObject.EmailExtraDto;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace College.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailRepo _email;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEncodeDecode _encode = new EncodeDecode();
        public EmailController(IWebHostEnvironment hostingEnvironment, IEmailRepo _email)
        {
            _hostingEnvironment = hostingEnvironment;
            this._email = _email;
        }

        private string EmailNormalBody(NormalEmailModelDto email)
        {
            var body = string.Empty;
            var folderPath = _hostingEnvironment.ContentRootPath + "\\Email\\normal_index.html";
            using (var reader = new StreamReader(folderPath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Title}", email.Title);
            body = body.Replace("{FavIconLink}", email.FavIconLink);
            body = body.Replace("{AppleTouchIconLink}", email.AppleTouchIconLink);
            body = body.Replace("{LogoImageLink}", email.LogoImageLink);
            body = body.Replace("{Purpose}", email.Purpose);
            body = body.Replace("{UserName}", email.UserName);
            body = body.Replace("{Message}", email.Message);
            body = body.Replace("{FooterCompany}", email.FooterCompany);
            body = body.Replace("{Company}", email.Company);
            body = body.Replace("{CopyRightYear}", email.CopyRightYear);
            body = body.Replace("{CompanyLink}", email.CompanyLink);
            body = body.Replace("{CompanyLinkText}", email.CompanyLinkText);
            body = body.Replace("{Warning}", email.Warning);
            body = body.Replace("{Description}", email.Description);
            body = body.Replace("{HomeText}", email.HomeText);
            body = body.Replace("{HomeLink}", email.HomeLink);
            body = body.Replace("{ServiceLink}", email.ServiceLink);
            body = body.Replace("{ServiceText}", email.ServiceText);
            body = body.Replace("{ContactLink}", email.ContactLink);
            body = body.Replace("{ContactText}", email.ContactText);
            return body;
        }

        private string EmailButtonBody(ButtonedEmailModelDto email)
        {
            var body = string.Empty;
            var folderPath = _hostingEnvironment.ContentRootPath + "\\Email\\button_index.html";
            using (var reader = new StreamReader(folderPath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Title}", email.Title);
            body = body.Replace("{FavIconLink}", email.FavIconLink);
            body = body.Replace("{AppleTouchIconLink}", email.AppleTouchIconLink);
            body = body.Replace("{LogoImageLink}", email.LogoImageLink);
            body = body.Replace("{Purpose}", email.Purpose);
            body = body.Replace("{UserName}", email.UserName);
            body = body.Replace("{Message}", email.Message);
            body = body.Replace("{FooterCompany}", email.FooterCompany);
            body = body.Replace("{Company}", email.Company);
            body = body.Replace("{CopyRightYear}", email.CopyRightYear);
            body = body.Replace("{CompanyLink}", email.CompanyLink);
            body = body.Replace("{CompanyLinkText}", email.CompanyLinkText);
            body = body.Replace("{Warning}", email.Warning);
            body = body.Replace("{Description}", email.Description);
            body = body.Replace("{HomeText}", email.HomeText);
            body = body.Replace("{HomeLink}", email.HomeLink);
            body = body.Replace("{ServiceLink}", email.ServiceLink);
            body = body.Replace("{ServiceText}", email.ServiceText);
            body = body.Replace("{ContactLink}", email.ContactLink);
            body = body.Replace("{ContactText}", email.ContactText);
            body = body.Replace("{ButtonUrl}", email.ButtonUrl);
            body = body.Replace("{ButtonText}", email.ButtonText);
            body = body.Replace("{BoodyWarningMessage}", email.BoodyWarningMessage);
            return body;
        }

        public async Task<bool> SendEmail(NormalEmailModelDto normal, ButtonedEmailModelDto buttoned,
            EmailUpdateModelDto emailModel)
        {
            if (emailModel == null)
                return false;
            var messages = new MimeMessage();
            var fromServer = new MailboxAddress(emailModel.From, emailModel.Email);
            messages.From.Add(fromServer);
            if (normal != null && buttoned == null)
            {
                var toClient = new MailboxAddress("User", normal.To);
                messages.To.Add(toClient);
                messages.Subject = normal.Subject;
                messages.Body = new TextPart(TextFormat.Html)
                {
                    Text = EmailNormalBody(normal)
                };
            }
            else if (normal == null && buttoned != null)
            {
                var toClient = new MailboxAddress("User", buttoned.To);
                messages.To.Add(toClient);
                messages.Subject = buttoned.Subject;
                messages.Body = new TextPart(TextFormat.Html)
                {
                    Text = EmailButtonBody(buttoned)
                };
            }

            var client = new SmtpClient();
            var mailServer = emailModel.MailServer;
            var port = emailModel.SmtpPort;
            var from = emailModel.Email;
            var password = _encode.Decrypt(emailModel.Password);
            await client.ConnectAsync(mailServer, port, true);
            await client.AuthenticateAsync(from, password);
            try
            {
                await client.SendAsync(messages);
                await client.DisconnectAsync(true);
                client.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        [AuthOverride]
        public async Task<IActionResult> Manager()
        {
            var emailConfigDbDetails = await _email.FetchEmailByFilter(1);
            return View(emailConfigDbDetails);
        }

        [AuthOverride]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manager(EmailUpdateModelDto emailUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var oldModel = await _email.FetchEmailByFilter(emailUpdateModel.EmailId);
                if (emailUpdateModel.Password == null) emailUpdateModel.Password = oldModel.Password;

                if (await _email.UpdateExistingEmailAsyncTask(emailUpdateModel))
                {
                    HttpContext.Session.SetString("Success", "Email Information Updated Successfully ");
                    return RedirectToAction(nameof(Manager));
                }

                HttpContext.Session.SetString("Error", "Problem connecting To server!");
                return RedirectToAction(nameof(Manager));
            }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return RedirectToAction(nameof(Manager));
        }

        [AuthOverride]
        public IActionResult Sender()
        {
            return View();
        }

        [AuthOverride]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sender(EmailSenderModelDto senderModel)
        {
            var builder = new RandomStringBuilder();
            if (ModelState.IsValid)
            {
                if (senderModel != null)
                {
                    var companyName = "SBDM Polytechnic Institute";
                    var emailConfigDbDetails = await _email.FetchEmailByFilter(1);
                    var normalEmailObject = new NormalEmailModelDto
                    {
                        Title = senderModel.Purpose,
                        FavIconLink = " http://bishnudhani.edu.np/favicon_io_Dark/favicon.ico",
                        AppleTouchIconLink = "http://bishnudhani.edu.np/favicon_io_Dark/apple-touch-icon.png",
                        LogoImageLink = "http://bishnudhani.edu.np/favicon_io_Dark/android-chrome-512x512.png",
                        Purpose = senderModel.Purpose,
                        UserName = "User",
                        Message = builder.StripHtml(senderModel.Message),
                        Company = companyName,
                        CopyRightYear = DateTime.UtcNow.Year.ToString(),
                        FooterCompany = companyName,
                        CompanyLink = "http://bishnudhani.edu.np",
                        CompanyLinkText = companyName,
                        Warning = "If you are not associated with this organization please ignore this email!",
                        Description =
                            "Shahid Bishnu Dhani Memorial is a newly established educational institution which primarily attempts to contribute to the development of the country through the production of skilled and semi-skilled human resources. ",
                        HomeText = "Home",
                        HomeLink = "http://bishnudhani.edu.np",
                        ContactText = "Forestry",
                        ContactLink = "http://bishnudhani.edu.np/Home/Forestry",
                        ServiceText = "Agriculture",
                        ServiceLink = "http://bishnudhani.edu.np/Home/Agriculture",
                        DisplayName = emailConfigDbDetails.From,
                        To = senderModel.To,
                        Subject = "Dear User, " + senderModel.Purpose +
                                  ". Shahid Bishnu Dhani Memorial Polytechnic Institute"
                    };
                    if (await SendEmail(normalEmailObject, null, emailConfigDbDetails))
                    {
                        HttpContext.Session.SetString("Success",
                            "Email has been sent to " + senderModel.To);
                        return RedirectToAction(nameof(Sender));
                    }

                    HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Again later!");
                    return RedirectToAction(nameof(Sender));
                }

                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Sender));
            }

            HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
            return RedirectToAction(nameof(Sender));
        }
    }
}