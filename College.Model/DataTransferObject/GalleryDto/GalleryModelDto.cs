using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.GalleryDto
{
    public class GalleryModelDto
    {
        public int GalleryId { get; set; }

        [Required]
        [DisplayName("Gallery Title")]
        public string Title { get; set; }

        public string Thumbnail { get; set; }

        [DisplayName("Thumbnail Image")] public IFormFile FileString { get; set; }

        public string Photographer { get; set; }
    }
}