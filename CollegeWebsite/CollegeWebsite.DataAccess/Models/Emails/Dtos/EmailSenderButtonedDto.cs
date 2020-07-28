namespace CollegeWebsite.DataAccess.Models.Emails.Dtos
{
    public class EmailSenderButtonedDto : EmailSenderNormalDto
    {
        public string ButtonUrl { get; set; }
        public string ButtonText { get; set; }
        public string BoodyWarningMessage { get; set; }
    }
}
