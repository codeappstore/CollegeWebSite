using College.Database.Helper;
using College.Model.DataTransferObject.EmailExtraDto;
using College.Model.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmailController(IWebHostEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        private string EmailNormalBody(NormalEmailModelDto email)
        {
            string body = string.Empty;
            var folderPath = _hostingEnvironment.ContentRootPath + "\\Email\\normal_index.html";
            using (StreamReader reader = new StreamReader(folderPath))
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
            body = body.Replace("{HomeLink}", email.HomeLink);
            body = body.Replace("{ServiceLink}", email.ServiceLink);
            body = body.Replace("{ServiceText}", email.ServiceText);
            body = body.Replace("{ContactLink}", email.ContactLink);
            body = body.Replace("{ContactText}", email.ContactText);
            return body;
        }

        private string EmailButtonBody(ButtonedEmailModelDto email)
        {
            string body = string.Empty;
            var folderPath = _hostingEnvironment.ContentRootPath + "\\Email\\button_index.html";
            using (StreamReader reader = new StreamReader(folderPath))
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

        public async Task<bool> SendEmail(NormalEmailModelDto normal, ButtonedEmailModelDto buttoned, EmailModel emailModel)
        {
            if (emailModel == null)
                return false;
            var messages = new MimeMessage();
            var fromServer = new MailboxAddress(emailModel.From, emailModel.Email);
            messages.From.Add(fromServer);
            var builder = new RandomStringBuilder();
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
            var password = builder.Decrypt(emailModel.Password);
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

    }
}
