using CollegeWebsite.DataAccess.Models.Emails.Dtos;

namespace CollegeWebsite.DataAccess.Models.Emails.Services.IRepo
{
    public interface IEmailConfigRepo
    {
        EmailConfigDto GetEmailConfiguration();
        string GetBaseUrl();
    }
}
