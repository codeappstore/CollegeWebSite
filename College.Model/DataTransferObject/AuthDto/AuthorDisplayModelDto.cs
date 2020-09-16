using System;
using College.Model.Models;

namespace College.Model.DataTransferObject.AuthDto
{
    public class AuthorDisplayModelDto
    {
        public int AuthId { get; set; }
        public int RoleId { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AuthGender Gender { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }
        public DateTime DateEmailVerified { get; set; }

        public IsAllowed Allowed { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}