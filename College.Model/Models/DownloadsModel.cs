using System;

namespace College.Model.Models
{
    public class DownloadsModel
    {
        public int FileId { get; set; }
        public string Title { get; set; }
        public string FileLink { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}