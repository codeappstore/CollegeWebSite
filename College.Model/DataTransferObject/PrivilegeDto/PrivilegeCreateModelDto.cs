using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.PrivilegeDto
{
    public class PrivilegeCreateModelDto
    {
        [Required] public int RoleId { get; set; }

        [DefaultValue(true)] public bool Read { get; set; }

        [DefaultValue(true)] public bool Create { get; set; }

        [DefaultValue(true)] public bool Update { get; set; }

        [DefaultValue(true)] public bool Delete { get; set; }
    }
}