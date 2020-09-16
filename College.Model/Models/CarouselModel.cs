using System;

namespace College.Model.Models
{
    public class CarouselModel
    {
        public int CarouselId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}