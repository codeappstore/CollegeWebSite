using System;

namespace College.Model.Models
{
    public class PageListModel
    {
        public int PageListId { get; set; }
        public string PageName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}