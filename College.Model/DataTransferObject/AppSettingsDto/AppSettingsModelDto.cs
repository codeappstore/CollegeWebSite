using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.AppSettingsDto
{
    public class AppSettingsModelDto
    {
        public int SettingsId { get; set; }

        [Display(Name = "Client Code")] public string ClientCode { get; set; }

        [Display(Name = "Project Code")] public string ProjectCode { get; set; }

        public string Certificate { get; set; }
        public string License { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Organization Name")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
    }
}