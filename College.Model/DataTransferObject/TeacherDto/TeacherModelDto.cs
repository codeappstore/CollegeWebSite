using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.TeacherDto
{
    public class TeacherModelDto
    {
        public int TeacherId { get; set; }

        [Required]
        [DisplayName("Staff's Name")]
        public string TeacherName { get; set; }

        [Required]
        [DisplayName("Staff's Post")]
        public string Designation { get; set; }

        public string Image { get; set; }

        [DisplayName("Image")] public IFormFile ImageString { get; set; }

        [DisplayName("Staff's Facebook")] public string Facebook { get; set; }

        [DisplayName("Staff's Instagram")] public string Instagram { get; set; }

        [DisplayName("Staff's Twitter")] public string Twitter { get; set; }


        [DisplayName("Staff's Google+")] public string GooglePlus { get; set; }
    }
}