using System;

namespace College.Model.Models
{
    public class ImportantLinks
    {
        public int ImportantLinkId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}