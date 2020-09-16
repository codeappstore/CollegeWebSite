using System;

namespace College.Model.DataTransferObject.AuthExtraDto
{
    public class ResetResponseModelDto
    {
        public string Email { get; set; }

        public string Token { get; set; }
        public DateTime IssuedDateTime { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}