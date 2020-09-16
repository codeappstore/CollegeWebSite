using System;
using College.Model.Models;

namespace College.Model.DataTransferObject.AuthDto
{
    public class AuthBasicDetailsModelDto
    {
        public int AuthId { get; set; }
        public int RoleId { get; set; }
        public int PrivilegeId { get; set; }

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

        public string RoleName { get; set; }
        public RoleStatus State { get; set; }
        public string Description { get; set; }

        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}