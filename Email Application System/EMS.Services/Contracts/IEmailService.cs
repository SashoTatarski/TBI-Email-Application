using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IEmailService
    {
        Task ChangeStatusAsync(string id, EmailStatus newStatus);
        Task<List<EmailDto>> GetAllEmailsAsync();
        Task<string> GetGmailIdAsync(string id);
        Task<EmailDto> GetSingleEmailAsync(string mailId);
        Task<string> GetBodyByGmailAsync(string messageId);
        Task<string> GetBodyByDbAsync(string emailId);
        Task<List<EmailDto>> GetEmailsAsync(EmailStatus status);
    }
}
