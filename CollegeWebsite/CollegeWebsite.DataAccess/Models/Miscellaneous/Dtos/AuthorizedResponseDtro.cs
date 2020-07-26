using System;

namespace CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos
{
    public class AuthorizedResponseDtro
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
