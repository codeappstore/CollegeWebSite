using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.PopupDto
{
    public class PopupModelDto
    {
        public int PopupId { get; set; }

        [Required] public string Name { get; set; }

        public string Link { get; set; }

        [DisplayName("Image")] public IFormFile ImageString { get; set; }
    }
}