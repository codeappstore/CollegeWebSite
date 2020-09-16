using System.ComponentModel.DataAnnotations;

namespace College.Model.DataTransferObject.FooterDto
{
    public class FooterUpdateModelDto
    {
        public int FooterHeaderId { get; set; }

        [Required] [Display(Name = "Slogan")] public string Slogan { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }


        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string ContactEmail { get; set; }

        [Required] [Display(Name = "Address")] public string ContactAddress { get; set; }

        [Display(Name = "Facebook")] public string FacebookLink { get; set; }

        [Display(Name = "Twitter")] public string TweeterLink { get; set; }

        [Display(Name = "Instagram")] public string InstaGramLink { get; set; }

        [Display(Name = "Google+")] public string GooglePlusLink { get; set; }

        [Display(Name = "Youtube")] public string YoutubeLink { get; set; }
    }
}