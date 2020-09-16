using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.AppSettingsDto;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using College.Model.Models;

namespace College.Access.IRepository
{
    public interface IAuthRepo
    {
        // Saves the current context 
        Task<bool> SaveChangesAsyncTask();

        // Check if email is valid
        Task<bool> IsValidEmail(string email);

        // Check if email is verified 
        Task<bool> IsVerifiedEmail(string email);

        // Checks if login credentials is valid
        Task<bool> IsValidLogInAsyncTask(LoginModelDto loginModel);

        // Remove invalid request
        Task<bool> RemoveInvalidResetRequest(ResetResponseModelDto resetResponse);

        // Receive Reset request
        Task<ResetResponseModelDto> ResetRequestAsyncTask(string email);

        // Check If the requested ResetInformation Id Valid
        Task<bool> ResetInformationValid(ResetResponseModelDto resetResponse);

        // Reset new password
        Task<AuthModel> ResetApprovedAsyncTask(ResetPasswordModelDto resetPasswordModel);

        // Verify Email request
        Task<EmailVerificationModelDto> VerifyEmailRequestAsyncTask(string email);

        // Email Verified
        Task<AuthModel> EmailVerifiedAsyncTask(EmailVerificationModelDto emailVerificationModel);

        // Create User
        Task<bool> CreateNewUserAsyncTask(AuthCreateModelDto authCreateModel);

        // Update User except email and password
        Task<bool> UpdateExistingUserAsyncTask(AuthUpdateModelDto authUpdateModel);

        // List User
        Task<IList<AuthorDisplayModelDto>> FetchUsersListAsyncTask(int exceptSelf);

        // User By Id Or User By email 
        Task<AuthUpdateModelDto> FetchUserByFilter(string email = null, int? id = null, string phoneNumber = null);

        // Delete User
        Task<bool> DeleteUserAsyncTask(int? id = null, string email = null);

        // User Exists
        Task<bool> UserExistsAsyncTask(int? id = null, string email = null);

        // Auth Role Privilege Basic  Details By Auth Email
        Task<AuthBasicDetailsModelDto> BasicUserDetailsAsyncTask(string email);

        // Check if username, email and Phone are unique
        Task<bool> IsUniqueUserCredentials(string email = null, string userName = null, string phoneNumber = null);

        // Counts of auth details
        int UserCount();
        int AdminCount();
        int DeveloperCount();
        int ManagerCount();

        //AppSettings
        Task<AppSettingsModelDto> FetchSettings();
        Task<bool> AddAppSettings(AppSettingsModelDto appSettings);
        Task<bool> UpdateAppSettings(string license, string certificate);
    }
}