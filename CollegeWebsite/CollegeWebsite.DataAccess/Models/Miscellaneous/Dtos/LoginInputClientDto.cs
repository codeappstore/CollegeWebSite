using System.ComponentModel.DataAnnotations;

namespace CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos
{
    public class LoginInputClientDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "Length must be between 8 to 50")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
