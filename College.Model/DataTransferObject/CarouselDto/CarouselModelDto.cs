using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.CarouselDto
{
    public class CarouselModelDto
    {
        public int CarouselId { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Summary { get; set; }

        public string Image { get; set; }

        [DisplayName("Image")] public IFormFile ImageString { get; set; }
    }
}