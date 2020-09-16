using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.AttachmentDto
{
    public class AttachmentModelDto
    {
        public int AttachmentId { get; set; }
        public int PageId { get; set; }
        public string Link { get; set; }

        [DisplayName("File")] public IFormFile FileString { get; set; }

        [DisplayName("File Name")] public string FileName { get; set; }
    }
}