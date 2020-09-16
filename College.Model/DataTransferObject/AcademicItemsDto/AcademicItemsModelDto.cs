using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.AcademicItemsDto
{
    public class AcademicItemsModelDto
    {
        public int ItemId { get; set; }
        public string Image { get; set; }

        [DisplayName("Image")] public IFormFile ImageString { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string Link { get; set; }
    }
}