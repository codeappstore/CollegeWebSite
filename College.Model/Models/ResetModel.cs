using System;

namespace College.Model.Models
{
    public class ResetModel
    {
        public int ResetId { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }
        public DateTime IssuedDateTime { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}