using CollegeWebsite.DataAccess.Models.Emails.Dtos;
using CollegeWebsite.DataAccess.Models.Emails.Services.IRepo;
using Microsoft.Extensions.Options;

namespace CollegeWebsite.DataAccess.Models.Emails.Services.Repo
{
    public class EmailConfigRepo : IEmailConfigRepo
    {
        private readonly IOptions<EmailConfigDto> emailConf;
        public EmailConfigRepo(IOptions<EmailConfigDto> emailConf)
        {
            this.emailConf = emailConf;
        }
        public EmailConfigDto GetEmailConfiguration()
        {
            EmailConfigDto client = new EmailConfigDto();
            client.MailServer = emailConf.Value.MailServer;
            client.SmtpPort = emailConf.Value.SmtpPort;
            client.Form = emailConf.Value.Form;
            client.UserName = emailConf.Value.UserName;
            client.Password = emailConf.Value.Password;
            client.BaseUrl = emailConf.Value.BaseUrl;
            client.ApiKey = emailConf.Value.ApiKey;
            client.ApiAccessUrl = emailConf.Value.ApiAccessUrl;

            return client;
        }

        public string GetBaseUrl()
        {
            return emailConf.Value.BaseUrl;
        }
    }
}
