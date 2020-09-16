using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.GalleryDto
{
    public class ImageModelDto
    {
        public int ImageId { get; set; }

        [DisplayName("Gallery")] public int GalleryId { get; set; }

        [DisplayName("Image")] public IFormFile FileString { get; set; }

        public string ImageLink { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
    }
}