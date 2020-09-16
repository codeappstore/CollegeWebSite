using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;

namespace College.Model.DataTransferObject.OtherDto
{
    public class AuthImageModelDto
    {
        public AuthCreateModelDto AuthModel { get; set; }
        public ImageModelDto ImageModel { get; set; }
    }
}