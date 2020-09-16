using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.AuthExtraDto
{
    public class ResetRequestModelDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
    }
}