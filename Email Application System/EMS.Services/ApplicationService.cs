using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Factories.Contracts;
using EMS.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly SystemDataContext _context;
        private readonly IApplicationFactory _factory;
        private readonly IUserService _userService;

        public ApplicationService(SystemDataContext context, IApplicationFactory factory, IUserService userService)
        {
            _context = context;
            _factory = factory;
            _userService = userService;
        }

        public async Task Delete(string appId)
        {
            var application = await GetApplication(appId);

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationDto> GetAppByMailIdAsync(string emailId)
        {
            var appDomain = await GetAppDomain(emailId);

            return appDomain.MapToDtoModel();
        }

        public async Task<string> GetAppIdByMailIdAsync(string emailId)
        {
            var appDomain = await GetAppDomain(emailId);

            return appDomain.Id.ToString();
        }

        public async Task CreateAsync(string emailId, string username, string EGN, string name, string phoneNum)
        {
            var userId = await _userService.GetUserIdAsync(username);
            var newApplication = _factory.Create(emailId, userId, EGN, name, phoneNum);

            _context.Applications.Add(newApplication);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<ApplicationDto>> GetOpenAppsAsync()
        {
            var appsDomain = await _context.Applications
                .Where(app => app.Status == ApplicationStatus.NotReviewed)
                .ToListAsync()
                .ConfigureAwait(false);

            var appsDto = new List<ApplicationDto>();
            foreach (var app in appsDomain)
            {
                appsDto.Add(app.MapToDtoModel());
            }

            return appsDto;
        }

        public async Task ChangeStatusAsync(string appId, ApplicationStatus newStatus, string operatorUsername)
        {
            var application = await GetApplication(appId);

            var userId = await _userService.GetUserIdAsync(operatorUsername);
            var user = await _userService.GetUserAsync(operatorUsername);

            bool currentStatus = application.Status == ApplicationStatus.Rejected || application.Status == ApplicationStatus.Approved;
            bool wantedStatus = newStatus == ApplicationStatus.Rejected || newStatus == ApplicationStatus.Approved;

            if (currentStatus && wantedStatus)
            {
                throw new ArgumentException("Someone did it before you do");
            }

            if (application.UserId != userId)
            {
                application.UserId = userId;
            }

            application.Status = newStatus;

            await _context.SaveChangesAsync();
        }

        public async Task<string> GetOperatorUsernameAsync(string emailId)
        {
            var application = await _context.Applications
                .Include(app => app.User)
                .FirstOrDefaultAsync(app => app.EmailId.ToString() == emailId)
                .ConfigureAwait(false);

            return application?.User.UserName;
        }

        public async Task<string> GetEmailIdAsync(string appId)
        {
            var application = await GetApplication(appId);

            return application.EmailId.ToString();
        }

        public async Task<string> GetStatusAsync(string emailId)
        {
            var application = await GetAppDomain(emailId);

            return application.Status.ToString();
        }

        private async Task<ApplicationDomain> GetApplication(string appId)
        {
            return await _context.Applications
                .FirstOrDefaultAsync(app => app.Id.ToString() == appId)
                .ConfigureAwait(false);
        }
        private async Task<ApplicationDomain> GetAppDomain(string emailId)
        {
            return await _context.Applications
                            .FirstOrDefaultAsync(app => app.EmailId.ToString() == emailId)
                            .ConfigureAwait(false);
        }
    }
}
