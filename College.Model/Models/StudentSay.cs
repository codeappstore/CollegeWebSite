using System;

namespace College.Model.Models
{
    public class StudentSay
    {
        public int StudentSayId { get; set; }
        public string Slogan { get; set; }
        public string BackgroundImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}