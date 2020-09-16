using System;

namespace College.Model.Models
{
    public class PopupModel
    {
        public int PopupId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}