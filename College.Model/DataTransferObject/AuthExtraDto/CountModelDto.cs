using System.ComponentModel;

namespace College.Model.DataTransferObject.AuthExtraDto
{
    public class CountModelDto
    {
        [DisplayName("Users")] public int Users { get; set; }

        [DisplayName("Administrators")] public int Administrator { get; set; }

        [DisplayName("Developers")] public int Developer { get; set; }

        [DisplayName("Managers")] public int Manager { get; set; }
    }
}