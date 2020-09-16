using System;

namespace College.Model.Models
{
    public enum RoleStatus
    {
        Disabled,
        Enabled
    }

    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public RoleStatus State { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}