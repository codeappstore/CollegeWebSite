using System;

namespace CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos
{
    public enum HasAccess
    {
        Disable,
        Enabled,
        Expired
    }
    public enum AuthGender
    {
        None,
        Male,
        Female,
        Others
    }
    public enum RoleState
    {
        Disabled,
        Enabled
    }

    public enum VendorAccess
    {
        Disable,
        Enabled,
        Expired
    }

    public class BasicDetailsDto
    {
        public string AuthId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string ImageURL { get; set; }

        public bool IsEmailVerified { get; set; }
        public DateTime DateEmailVerified { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public HasAccess Access { get; set; }
        public string RoleId { get; set; }
        public string DbConfigId { get; set; }

        public string AuthDetailId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AuthGender Gender { get; set; }
        public string Address { get; set; }

        public string RoleName { get; set; }
        public string PrivilegeId { get; set; }
        public RoleState State { get; set; }
        public string Description { get; set; }

        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public string VendorURL { get; set; }
        public VendorAccess VendorAccess { get; set; }
    }
}
