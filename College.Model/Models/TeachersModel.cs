using System;

namespace College.Model.Models
{
    public class TeachersModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Designation { get; set; }
        public string Image { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string GooglePlus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}