using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.StudentSayStudents
{
    public class StudentSayStudentsModelDto
    {
        public int StudentSayId { get; set; }

        [Required]
        [DisplayName("Students Name")]
        public string StudentName { get; set; }

        [Required]
        [DisplayName("Students Designation")]
        public string StudentDesignation { get; set; }

        [DisplayName("Students Image")] public IFormFile ImageString { get; set; }

        public string Image { get; set; }

        [Required]
        [DisplayName("Students Saying")]
        public string Description { get; set; }
    }
}