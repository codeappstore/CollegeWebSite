using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace College.Model.DataTransferObject.AuthExtraDto
{
    public class ImageModelDto
    {
        [DisplayName("Profile Image")] public IFormFile Image { get; set; }
    }
}