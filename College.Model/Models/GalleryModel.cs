using System;

namespace College.Model.Models
{
    public class GalleryModel
    {
        public int GalleryId { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Photographer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}