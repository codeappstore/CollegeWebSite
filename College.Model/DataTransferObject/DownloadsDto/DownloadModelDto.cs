using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.DownloadsDto
{
    public class DownloadModelDto
    {
        public int FileId { get; set; }

        [Required] [DisplayName("File Name")] public string Title { get; set; }

        public string FileLink { get; set; }

        [DisplayName("File")] public IFormFile FileString { get; set; }

        public string Size { get; set; }
        public string Extension { get; set; }
    }
}