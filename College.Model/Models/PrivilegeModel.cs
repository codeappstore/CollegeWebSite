using System;

namespace College.Model.Models
{
    public class PrivilegeModel
    {
        public int PrivilegeId { get; set; }
        public int RoleId { get; set; }

        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}