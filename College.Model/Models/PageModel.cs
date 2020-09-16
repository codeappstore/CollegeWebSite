using System;

namespace College.Model.Models
{
    public class PageModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string BackgroundImage { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}