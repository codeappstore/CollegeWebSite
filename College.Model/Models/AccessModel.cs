using System;

namespace College.Model.Models
{
    public enum AccessStatus
    {
        Disable,
        Enabled
    }

    public class AccessModel
    {
        public int AccessId { get; set; }
        public int RoleId { get; set; }
        public int PageListId { get; set; }

        public AccessStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}