using EMS.Data.Enums;
using EMS.Services.dto_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IApplicationService
    {
        Task ChangeStatusAsync(string applicationId, ApplicationStatus newStatus, string operatorUsername);
        Task CreateAsync(string emailId, string userId, string EGN, string name, string phoneNum);
        Task<ApplicationDto> GetAppByMailIdAsync(string emailId);
        Task Delete(string appId);
        Task<List<ApplicationDto>> GetOpenAppsAsync();
        Task<string> GetOperatorUsernameAsync(string emailId);
        Task<string> GetEmailIdAsync(string appId);
        Task<string> GetStatusAsync(string mailId);
        Task<string> GetAppIdByMailIdAsync(string emailId);
    }
}
