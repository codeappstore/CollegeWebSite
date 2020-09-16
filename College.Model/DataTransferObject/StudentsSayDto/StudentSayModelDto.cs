using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.StudentsSayDto
{
    public class StudentSayModelDto
    {
        public int StudentSayId { get; set; }

        [Required] public string Slogan { get; set; }

        [Required]
        [DisplayName("Background Image")]
        public IFormFile BackgroundImage { get; set; }

        public string Image { get; set; }
    }
}