using CodeAppStore.License.EncodeDecodeRepo;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.EmailDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Access.Repository
{
    public class EmailRepo : IEmailRepo
    {
        private readonly CollegeContext _context;
        readonly IEncodeDecode _encode = new EncodeDecode();

        public EmailRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> AddNewEmailAsyncTask(EmailCreateModelDto emailCreateModel)
        {
            if (emailCreateModel == null)
                throw new ArgumentNullException(nameof(emailCreateModel));
            await _context.Email.AddAsync(new EmailModel
            {
                Email = emailCreateModel.Email,
                Password = _encode.Encrypt(emailCreateModel.Password),
                From = emailCreateModel.From,
                SmtpPort = emailCreateModel.SmtpPort,
                MailServer = emailCreateModel.MailServer,
                IsAvailable = emailCreateModel.IsAvailable,
                IsMasterEmail = emailCreateModel.IsMasterEmail,
                CreatedDate = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateExistingEmailAsyncTask(EmailUpdateModelDto emailUpdateModel)
        {
            if (emailUpdateModel == null)
                throw new ArgumentNullException(nameof(emailUpdateModel));
            _context.Email.Update(new EmailModel
            {
                EmailId = emailUpdateModel.EmailId,
                Email = emailUpdateModel.Email,
                Password = _encode.Encrypt(emailUpdateModel.Password),
                From = emailUpdateModel.From,
                SmtpPort = emailUpdateModel.SmtpPort,
                MailServer = emailUpdateModel.MailServer,
                IsAvailable = emailUpdateModel.IsAvailable,
                IsMasterEmail = emailUpdateModel.IsMasterEmail,
                DateUpdated = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<IList<EmailModel>> FetchEmailListAsyncTask()
        {
            return await _context.Email.OrderByDescending(a => a.CreatedDate).ToListAsync();
        }

        public async Task<EmailUpdateModelDto> FetchEmailByFilter(int? id = null, string email = null)
        {
            if (!string.IsNullOrWhiteSpace(email) && id == null)
                return await (from em in _context.Email
                              select new EmailUpdateModelDto
                              {
                                  EmailId = em.EmailId,
                                  Email = em.Email,
                                  Password = em.Password,
                                  From = em.From,
                                  SmtpPort = em.SmtpPort,
                                  MailServer = em.MailServer,
                                  IsAvailable = em.IsAvailable,
                                  IsMasterEmail = em.IsMasterEmail
                              }).FirstOrDefaultAsync(e => e.Email == email);
            if (string.IsNullOrWhiteSpace(email) && id != null)
                return await (from em in _context.Email
                              select new EmailUpdateModelDto
                              {
                                  EmailId = em.EmailId,
                                  Email = em.Email,
                                  Password = em.Password,
                                  From = em.From,
                                  SmtpPort = em.SmtpPort,
                                  MailServer = em.MailServer,
                                  IsAvailable = em.IsAvailable,
                                  IsMasterEmail = em.IsMasterEmail
                              }).FirstOrDefaultAsync(e => e.EmailId == id);
            throw new ArgumentNullException();
        }

        public async Task<bool> DeleteEmailAsyncTask(int id)
        {
            var emailModel = await _context.Email.FirstOrDefaultAsync(a => a.EmailId == id);
            _context.Email.Remove(emailModel);
            return await SaveChangesAsyncTask();
        }
    }
}