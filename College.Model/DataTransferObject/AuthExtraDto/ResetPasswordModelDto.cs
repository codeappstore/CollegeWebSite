using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.AuthExtraDto
{
    public class ResetPasswordModelDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Length must be between 8 to 50")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Length must be between 8 to 50")]
        public string ConfirmPassword { get; set; }
    }
}