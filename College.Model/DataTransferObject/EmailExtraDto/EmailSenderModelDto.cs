using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace College.Model.DataTransferObject.EmailExtraDto
{
    public class EmailSenderModelDto
    {
        [Required] public string Purpose { get; set; }

        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the receivers email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string To { get; set; }

        [Required] [AllowHtml] public string Message { get; set; }
    }
}