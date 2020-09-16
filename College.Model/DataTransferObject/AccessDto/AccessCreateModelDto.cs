using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using College.Model.Models;

namespace College.Model.DataTransferObject.AccessDto
{
    public class AccessCreateModelDto
    {
        [Required] public int RoleId { get; set; }

        [Required] public int PageListId { get; set; }

        [DefaultValue(AccessStatus.Enabled)] public AccessStatus Status { get; set; }
    }
}