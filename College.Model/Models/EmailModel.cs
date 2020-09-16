using System;

namespace College.Model.Models
{
    public class EmailModel
    {
        public int EmailId { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsMasterEmail { get; set; }
        public string MailServer { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public int SmtpPort { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}