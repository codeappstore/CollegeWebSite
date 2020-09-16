using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.PageDto
{
    public class PageModelDto
    {
        public int PageId { get; set; }

        [Required] [DisplayName("Title")] public string PageName { get; set; }

        public string BackgroundImage { get; set; }

        [DisplayName("Background Image")] public IFormFile ImageString { get; set; }

        [DisplayName("Description")]
        [Required]
        public string Description { get; set; }
    }
}