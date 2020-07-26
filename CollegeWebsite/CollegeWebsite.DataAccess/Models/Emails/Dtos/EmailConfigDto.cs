namespace CollegeWebsite.DataAccess.Models.Emails.Dtos
{
    public class EmailConfigDto
    {
        public string MailServer { get; set; }
        public int SmtpPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Form { get; set; }
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiAccessUrl { get; set; }
    }
}
