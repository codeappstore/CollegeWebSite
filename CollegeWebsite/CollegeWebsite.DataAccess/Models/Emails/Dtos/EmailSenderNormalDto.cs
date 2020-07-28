namespace CollegeWebsite.DataAccess.Models.Emails.Dtos
{
    public class EmailSenderNormalDto
    {
        public string Title { get; set; }
        public string FavIconLink { get; set; }
        public string AppleTouchIconLink { get; set; }

        public string LogoImageLink { get; set; }
        public string Purpose { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }


        public string Company { get; set; }
        public string CopyRightYear { get; set; }
        public string FooterCompany { get; set; }

        public string CompanyLink { get; set; }
        public string CompanyLinkText { get; set; }
        public string Warning { get; set; }

        public string Description { get; set; }

        public string HomeLink { get; set; }
        public string HomeText { get; set; }

        public string ContactLink { get; set; }
        public string ContactText { get; set; }

        public string ServiceLink { get; set; }
        public string ServiceText { get; set; }

        public string MailServer { get; set; }
        public string Password { get; set; }
        public int SmtpPort { get; set; }

        public string From { get; set; }
        public string DisplayName { get; set; }

        public string To { get; set; }
        public string Subject { get; set; }
    }
}
