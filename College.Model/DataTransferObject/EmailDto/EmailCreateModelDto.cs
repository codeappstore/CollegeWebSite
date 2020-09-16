using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.EmailDto
{
    public class EmailCreateModelDto
    {
        public bool? IsAvailable { get; set; }
        public bool? IsMasterEmail { get; set; }
        public string MailServer { get; set; }

        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [MinLength(8,
            ErrorMessage =
                "Password Must be of 8 characters. Strong passwords includes Alphabet[A-z] numbers 0-9 and special characters like @-/~")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required] public string From { get; set; }

        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Port number should not contain characters")]
        [Display(Name = "Port Number")]
        public int SmtpPort { get; set; }
    }
}