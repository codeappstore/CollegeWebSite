using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using College.Model.Models;

namespace College.Model.DataTransferObject.RoleDto
{
    public class RoleCreateModelDto
    {
        [Required] public string RoleName { get; set; }

        [DefaultValue(RoleStatus.Enabled)] public RoleStatus State { get; set; }

        public string Description { get; set; }
    }
}