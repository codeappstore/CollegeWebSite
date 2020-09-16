using System;

namespace College.Model.Models
{
    public class StudentsSayStudents
    {
        public int StudentSayId { get; set; }
        public string StudentName { get; set; }
        public string StudentDesignation { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}