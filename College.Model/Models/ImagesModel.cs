using System;

namespace College.Model.Models
{
    public class ImagesModel
    {
        public int ImageId { get; set; }
        public int GalleryId { get; set; }
        public string ImageLink { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}