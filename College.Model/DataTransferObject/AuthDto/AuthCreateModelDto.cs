using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using College.Model.Models;

namespace College.Model.DataTransferObject.AuthDto
{
    public class AuthCreateModelDto
    {
        [Required]
        [Display(Name = "User Roles")]
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.ImageUrl)] public string Image { get; set; }

        [Required]
        [Key]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [DefaultValue(AuthGender.None)]
        public AuthGender Gender { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Key]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [MinLength(8,
            ErrorMessage =
                "Password Must be of 8 characters. Strong passwords includes Alphabet[A-z] numbers 0-9 and special characters like @-/~")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsEmailVerified { get; set; }
        public DateTime DateEmailVerified { get; set; }


        [DefaultValue(AccessStatus.Enabled)] public IsAllowed Allowed { get; set; }
    }
}