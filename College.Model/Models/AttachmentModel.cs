using System;

namespace College.Model.Models
{
    public class AttachmentModel
    {
        public int AttachmentId { get; set; }
        public int PageId { get; set; }
        public string Link { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}