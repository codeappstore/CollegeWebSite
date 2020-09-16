using CodeAppStore.License.EncodeDecodeRepo;
using CodeAppStore.License.RandomStringRepo;
using College.Access.IRepository;
using College.Database.Context;
using College.Database.Helper;
using College.Model.DataTransferObject.AppSettingsDto;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace College.Access.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly CollegeContext _context;
        private readonly IEncodeDecode _encode = new EncodeDecode();
        private readonly IRandomString _random = new RandomString();

        public AuthRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> IsValidEmail(string email)
        {
            return await _context.Authentications.AnyAsync(e => e.Email == email);
        }

        public async Task<bool> IsVerifiedEmail(string email)
        {
            return await _context.Authentications.AnyAsync(a => a.Email == email && a.IsEmailVerified);
        }

        public async Task<bool> IsValidLogInAsyncTask(LoginModelDto loginModel)
        {
            if (loginModel == null)
                throw new ArgumentNullException(nameof(loginModel));
            if (string.IsNullOrWhiteSpace(loginModel.Email) && string.IsNullOrWhiteSpace(loginModel.Password))
                throw new ArgumentNullException(nameof(loginModel));
            try
            {
                var sweet = "CredentialCodeAppStoreDevSalt@020";
                var beforeHashed = sweet + loginModel.Password + loginModel.Email + loginModel.Password;
                var sha256 = SHA256.Create();
                var convertHashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(beforeHashed));
                var password = BitConverter.ToString(convertHashed).Replace("-", "").ToLower();
                var result = await _context.Authentications.AnyAsync(a =>
                    a.Email == loginModel.Email && a.Password == password);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentOutOfRangeException(nameof(e));
            }
        }

        public async Task<bool> RemoveInvalidResetRequest(ResetResponseModelDto resetResponse)
        {
            var resetModel = await _context.Resets.FirstOrDefaultAsync(a => a.Email == resetResponse.Email);
            _context.Resets.Remove(resetModel);
            return await SaveChangesAsyncTask();
        }

        public async Task<ResetResponseModelDto> ResetRequestAsyncTask(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            if (!await IsValidEmail(email))
                throw new ArgumentOutOfRangeException(nameof(email));
            var response = new ResetResponseModelDto
            {
                Email = email,
                Token = _random.RandomStringGenerator((int)RandomStringBuilder.PurposeOfString.TOKEN),
                IssuedDateTime = DateTime.Now,
                ExpirationDateTime = DateTime.Now.AddMinutes(20)
            };
            try
            {
                await _context.Resets.AddAsync(new ResetModel
                {
                    Email = response.Email,
                    Token = response.Token,
                    ExpirationDateTime = response.ExpirationDateTime,
                    IssuedDateTime = response.IssuedDateTime
                });
                if (!await SaveChangesAsyncTask())
                    throw new ArgumentOutOfRangeException(nameof(email));
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentOutOfRangeException(nameof(email));
            }
        }

        public async Task<bool> ResetInformationValid(ResetResponseModelDto resetResponse)
        {
            return await _context.Resets.AnyAsync(r =>
                r.Email == resetResponse.Email &&
                r.Token == resetResponse.Token);
        }

        public async Task<AuthModel> ResetApprovedAsyncTask(ResetPasswordModelDto resetPasswordModel)
        {
            if (resetPasswordModel == null)
                throw new ArgumentNullException(nameof(resetPasswordModel));
            if (string.IsNullOrWhiteSpace(resetPasswordModel.Email) &&
                string.IsNullOrWhiteSpace(resetPasswordModel.NewPassword))
                throw new ArgumentNullException(nameof(resetPasswordModel));
            var resetModel = await _context.Resets.FirstOrDefaultAsync(r => r.Email == resetPasswordModel.Email);
            if (resetModel == null)
                throw new ArgumentNullException(nameof(resetPasswordModel));
            var authModel =
                await _context.Authentications.FirstOrDefaultAsync(r => r.Email == resetPasswordModel.Email);
            if (authModel == null)
                throw new ArgumentNullException(nameof(authModel));
            var sweet = "CredentialCodeAppStoreDevSalt@020";
            var beforeHashed = sweet + resetPasswordModel.NewPassword + resetPasswordModel.Email +
                               resetPasswordModel.NewPassword;
            var sha256 = SHA256.Create();
            var convertHashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(beforeHashed));
            var password = BitConverter.ToString(convertHashed).Replace("-", "").ToLower();
            authModel.Password = password;
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Resets.Remove(resetModel);
                await _context.SaveChangesAsync();
                _context.Authentications.Update(authModel);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return authModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await transaction.RollbackAsync();
                throw new ArgumentOutOfRangeException(nameof(e));
            }
        }

        public async Task<EmailVerificationModelDto> VerifyEmailRequestAsyncTask(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            if (await IsVerifiedEmail(email))
                return null;
            return new EmailVerificationModelDto
            {
                Email = email,
                Token = _random.RandomStringGenerator((int)RandomStringBuilder.PurposeOfString.TOKEN)
            };
        }

        public async Task<AuthModel> EmailVerifiedAsyncTask(EmailVerificationModelDto emailVerificationModel)
        {
            if (emailVerificationModel == null)
                throw new ArgumentNullException(nameof(emailVerificationModel));
            if (string.IsNullOrWhiteSpace(emailVerificationModel.Token) &&
                string.IsNullOrWhiteSpace(emailVerificationModel.Email))
                throw new ArgumentNullException(nameof(emailVerificationModel));
            var authModel =
                await _context.Authentications.FirstOrDefaultAsync(r => r.Email == emailVerificationModel.Email);
            if (authModel == null)
                throw new ArgumentNullException(nameof(authModel));
            authModel.IsEmailVerified = true;
            authModel.DateEmailVerified = DateTime.Now;
            try
            {
                _context.Authentications.Update(authModel);
                if (!await SaveChangesAsyncTask())
                    throw new ArgumentNullException(nameof(authModel));
                return authModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentOutOfRangeException(nameof(e));
            }
        }

        public async Task<bool> CreateNewUserAsyncTask(AuthCreateModelDto authCreateModel)
        {
            if (authCreateModel == null)
                throw new ArgumentNullException(nameof(authCreateModel));
            var sweet = "CredentialCodeAppStoreDevSalt@020";
            var beforeHashed = sweet + authCreateModel.Password + authCreateModel.Email + authCreateModel.Password;
            var sha256 = SHA256.Create();
            var convertHashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(beforeHashed));
            var password = BitConverter.ToString(convertHashed).Replace("-", "").ToLower();
            await _context.Authentications.AddAsync(new AuthModel
            {
                RoleId = authCreateModel.RoleId,
                FullName = authCreateModel.FullName,
                UserName = authCreateModel.UserName,
                Image = authCreateModel.Image,
                PhoneNumber = authCreateModel.PhoneNumber,
                DateOfBirth = authCreateModel.DateOfBirth,
                Gender = authCreateModel.Gender,
                Email = authCreateModel.Email,
                Password = password,
                Address = authCreateModel.Address,
                IsEmailVerified = authCreateModel.IsEmailVerified,
                DateEmailVerified = authCreateModel.DateEmailVerified,
                Allowed = authCreateModel.Allowed,
                CreatedDate = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateExistingUserAsyncTask(AuthUpdateModelDto authUpdateModel)
        {
            if (authUpdateModel == null)
                throw new ArgumentNullException(nameof(authUpdateModel));
            if (await UserExistsAsyncTask(authUpdateModel.AuthId))
            {
                var authorModel = new AuthModel
                {
                    AuthId = authUpdateModel.AuthId,
                    RoleId = authUpdateModel.RoleId,
                    FullName = authUpdateModel.FullName,
                    UserName = authUpdateModel.UserName,
                    Image = authUpdateModel.Image,
                    PhoneNumber = authUpdateModel.PhoneNumber,
                    Email = authUpdateModel.Email,
                    Password = authUpdateModel.Password,
                    DateOfBirth = authUpdateModel.DateOfBirth,
                    Gender = authUpdateModel.Gender,
                    Address = authUpdateModel.Address,
                    IsEmailVerified = authUpdateModel.IsEmailVerified,
                    DateEmailVerified = authUpdateModel.DateEmailVerified,
                    Allowed = authUpdateModel.Allowed,
                    DateUpdated = DateTime.Now
                };
                _context.Authentications.Update(authorModel);
                return await SaveChangesAsyncTask();
            }

            throw new ArgumentNullException();
        }

        public async Task<IList<AuthorDisplayModelDto>> FetchUsersListAsyncTask(int exceptSelf)
        {
            return await (from auth in _context.Authentications
                          select new AuthorDisplayModelDto
                          {
                              AuthId = auth.AuthId,
                              RoleId = auth.RoleId,
                              FullName = auth.FullName,
                              UserName = auth.UserName,
                              Image = auth.Image,
                              PhoneNumber = auth.PhoneNumber,
                              DateOfBirth = auth.DateOfBirth,
                              Gender = auth.Gender,
                              Address = auth.Address,
                              Email = auth.Email,
                              IsEmailVerified = auth.IsEmailVerified,
                              DateEmailVerified = auth.DateEmailVerified,
                              Allowed = auth.Allowed,
                              CreatedDate = auth.CreatedDate,
                              DateUpdated = auth.DateUpdated
                          }).Where(a => a.RoleId != 2).Where(a => a.AuthId != exceptSelf).ToListAsync();
        }

        public async Task<AuthUpdateModelDto> FetchUserByFilter(string email = null, int? id = null,
            string phoneNumber = null)
        {
            if (!string.IsNullOrWhiteSpace(email) && id == null && string.IsNullOrWhiteSpace(phoneNumber))
                return await (from auth in _context.Authentications
                              select new AuthUpdateModelDto
                              {
                                  AuthId = auth.AuthId,
                                  RoleId = auth.RoleId,
                                  FullName = auth.FullName,
                                  UserName = auth.UserName,
                                  Image = auth.Image,
                                  PhoneNumber = auth.PhoneNumber,
                                  DateOfBirth = auth.DateOfBirth,
                                  Gender = auth.Gender,
                                  Address = auth.Address,
                                  Email = auth.Email,
                                  Password = auth.Password,
                                  IsEmailVerified = auth.IsEmailVerified,
                                  DateEmailVerified = auth.DateEmailVerified,
                                  Allowed = auth.Allowed
                              }).FirstOrDefaultAsync(e => e.Email == email);
            if (string.IsNullOrWhiteSpace(email) && id != null && string.IsNullOrWhiteSpace(phoneNumber))
                return await (from auth in _context.Authentications
                              select new AuthUpdateModelDto
                              {
                                  AuthId = auth.AuthId,
                                  RoleId = auth.RoleId,
                                  FullName = auth.FullName,
                                  UserName = auth.UserName,
                                  Image = auth.Image,
                                  PhoneNumber = auth.PhoneNumber,
                                  DateOfBirth = auth.DateOfBirth,
                                  Gender = auth.Gender,
                                  Address = auth.Address,
                                  Email = auth.Email,
                                  Password = auth.Password,
                                  IsEmailVerified = auth.IsEmailVerified,
                                  DateEmailVerified = auth.DateEmailVerified,
                                  Allowed = auth.Allowed
                              }).FirstOrDefaultAsync(e => e.AuthId == id);
            if (string.IsNullOrWhiteSpace(email) && id == null && !string.IsNullOrWhiteSpace(phoneNumber))
                return await (from auth in _context.Authentications
                              select new AuthUpdateModelDto
                              {
                                  AuthId = auth.AuthId,
                                  RoleId = auth.RoleId,
                                  FullName = auth.FullName,
                                  UserName = auth.UserName,
                                  Image = auth.Image,
                                  PhoneNumber = auth.PhoneNumber,
                                  DateOfBirth = auth.DateOfBirth,
                                  Gender = auth.Gender,
                                  Address = auth.Address,
                                  Email = auth.Email,
                                  Password = auth.Password,
                                  IsEmailVerified = auth.IsEmailVerified,
                                  DateEmailVerified = auth.DateEmailVerified,
                                  Allowed = auth.Allowed
                              }).FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber);
            throw new ArgumentNullException();
        }

        public async Task<bool> DeleteUserAsyncTask(int? id = null, string email = null)
        {
            if (id == null && !string.IsNullOrWhiteSpace(email))
            {
                if (await UserExistsAsyncTask(null, email))
                {
                    var authModel = await _context.Authentications.FirstOrDefaultAsync(a => a.Email == email);
                    _context.Authentications.Remove(authModel);
                    return await SaveChangesAsyncTask();
                }

                throw new ArgumentNullException();
            }

            if (id != null && string.IsNullOrWhiteSpace(email))
            {
                if (await UserExistsAsyncTask(id))
                {
                    var authModel = await _context.Authentications.FirstOrDefaultAsync(a => a.AuthId == id);
                    _context.Authentications.Remove(authModel);
                    return await SaveChangesAsyncTask();
                }

                throw new ArgumentNullException();
            }

            throw new ArgumentNullException();
        }

        public async Task<bool> UserExistsAsyncTask(int? id = null, string email = null)
        {
            if (id == null && !string.IsNullOrWhiteSpace(email))
                return await _context.Authentications.AnyAsync(a => a.Email == email);
            if (id != null && string.IsNullOrWhiteSpace(email))
                return await _context.Authentications.AnyAsync(a => a.AuthId == id);
            throw new ArgumentNullException();
        }

        public async Task<AuthBasicDetailsModelDto> BasicUserDetailsAsyncTask(string email)
        {
            var authBasic = new AuthBasicDetailsModelDto();
            authBasic = await (from authentications in _context.Authentications
                               join role in _context.Roles on authentications.RoleId equals role.RoleId
                               join privilege in _context.Privileges on role.RoleId equals privilege.RoleId
                               select new AuthBasicDetailsModelDto
                               {
                                   AuthId = authentications.AuthId,
                                   FullName = authentications.FullName,
                                   UserName = authentications.UserName,
                                   Image = authentications.Image,
                                   Email = authentications.Email,
                                   PhoneNumber = authentications.PhoneNumber,
                                   DateOfBirth = authentications.DateOfBirth,
                                   Gender = authentications.Gender,
                                   Address = authentications.Address,
                                   IsEmailVerified = authentications.IsEmailVerified,
                                   RoleId = authentications.RoleId,
                                   DateEmailVerified = authentications.DateEmailVerified,
                                   Allowed = authentications.Allowed,


                                   PrivilegeId = privilege.PrivilegeId,
                                   Description = role.Description,
                                   RoleName = role.RoleName,
                                   State = role.State,

                                   Create = privilege.Create,
                                   Read = privilege.Read,
                                   Update = privilege.Update,
                                   Delete = privilege.Delete
                               }).FirstOrDefaultAsync(auth => auth.Email == email);
            return authBasic;
        }

        public async Task<bool> IsUniqueUserCredentials(string email = null, string userName = null,
            string phoneNumber = null)
        {
            return await _context.Authentications.AnyAsync(a =>
                a.Email != email &&
                a.UserName != userName &&
                a.PhoneNumber != phoneNumber);
        }

        public int UserCount()
        {
            return _context.Authentications.ToList().Count;
        }

        public int AdminCount()
        {
            return _context.Authentications.Where(a => a.RoleId == 1).ToList().Count;
        }

        public int DeveloperCount()
        {
            return _context.Authentications.Where(a => a.RoleId == 2).ToList().Count;
        }

        public int ManagerCount()
        {
            return _context.Authentications.Where(a => a.RoleId == 3).ToList().Count;
        }

        public async Task<AppSettingsModelDto> FetchSettings()
        {
            try
            {
                return await (from set in _context.AppSettings
                              select new AppSettingsModelDto
                              {
                                  SettingsId = set.SettingsId,
                                  Certificate =
                                      string.IsNullOrWhiteSpace(set.Certificate) ? "" : _encode.Decrypt(set.Certificate),
                                  License = string.IsNullOrWhiteSpace(set.License) ? "" : _encode.Decrypt(set.License),
                                  ClientCode = string.IsNullOrWhiteSpace(set.ClientCode) ? "" : _encode.Decrypt(set.ClientCode),
                                  OrganizationName = string.IsNullOrWhiteSpace(set.OrganizationName)
                                      ? ""
                                      : _encode.Decrypt(set.OrganizationName),
                                  Email = string.IsNullOrWhiteSpace(set.Email)
                                      ? ""
                                      : _encode.Decrypt(set.Email),
                                  ProjectCode = string.IsNullOrWhiteSpace(set.ProjectCode) ? "" : _encode.Decrypt(set.ProjectCode)
                              }).OrderByDescending(a => a.SettingsId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> AddAppSettings(AppSettingsModelDto appSettings)
        {
            var appSettingsToAdd = new AppSettingsModel
            {
                ClientCode = _encode.Encrypt(appSettings.ClientCode),
                ProjectCode = _encode.Encrypt(appSettings.ProjectCode),
                OrganizationName = _encode.Encrypt(appSettings.OrganizationName),
                Email = _encode.Encrypt(appSettings.Email)
            };
            await _context.AppSettings.AddAsync(appSettingsToAdd);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> UpdateAppSettings(string license, string certificate)
        {
            try
            {
                var dataToUpdate = await _context.AppSettings.FirstOrDefaultAsync();
                dataToUpdate.License = _encode.Encrypt(license);
                dataToUpdate.Certificate = _encode.Encrypt(certificate);
                return await _context.SaveChangesAsync() >= 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}