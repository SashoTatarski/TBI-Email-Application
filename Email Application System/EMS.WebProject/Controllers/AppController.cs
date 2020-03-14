using EMS.Data;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Applications;
using EMS.WebProject.Models.Emails;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Policy = Constants.AuthPolicy)]
    [Authorize(Roles = "manager, operator")]
    public class AppController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public AppController(IApplicationService appService, IEmailService emailService, ILogger<EmailController> logger)
        {
            _appService = appService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> MarkAppNew(string id)
        {
            try
            {
                var emailId = await _appService.GetEmailIdAsync(id);

                await _appService.Delete(id);
                _logger.LogInformation(string.Format(Constants.LogAppDelete, User.Identity.Name, id));

                await _emailService.ChangeStatusAsync(emailId, EmailStatus.New);
                _logger.LogInformation(string.Format(Constants.LogEmailNew, User.Identity.Name, emailId));

                TempData[Constants.TempDataMsg] = Constants.AppNewSucc;

                return RedirectToAction(Constants.PageGetNewEmails, Constants.PageEmail);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Preview(string id)
        {
            try
            {
                var application = await _appService.GetAppByMailIdAsync(id);
                var app = application.MapToViewModelPreview();
                app.OperatorName = await _appService.GetOperatorUsernameAsync(id);

                var email = await _emailService.GetSingleEmailAsync(id);
                var attachmentViewModel = EmailController.MapAttachments(email);               

                var sanitizedBody = EmailController.SanitizeContent(email.Body);

                var vm = email.MapToViewModelPreview(sanitizedBody, attachmentViewModel);
                vm.Application = app;

                return View(vm);
            }

            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Approve(string id)
        {
            try
            {
                return await DecideAnApplication(id, ApplicationStatus.Approved, Constants.LogAppApproved);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        private async Task<IActionResult> DecideAnApplication(string id, ApplicationStatus status, string loggingInfo)
        {
            await _appService.ChangeStatusAsync(id, status, User.Identity.Name);
            _logger.LogInformation(string.Format(loggingInfo, User.Identity.Name, id));

            var emailId = await _appService.GetEmailIdAsync(id);
            await _emailService.ChangeStatusAsync(emailId, EmailStatus.Closed);
            _logger.LogInformation(string.Format(Constants.LogEmailClosed, User.Identity.Name, emailId));

            TempData[Constants.TempDataMsg] = status == ApplicationStatus.Approved ? Constants.AppValidSucc : Constants.AppInvalidSucc;

            return RedirectToAction(Constants.PageGetClosedEmails, Constants.PageEmail);
        }

        [HttpGet]
        public async Task<IActionResult> Reject(string id)
        {
            try
            {
                return await DecideAnApplication(id, ApplicationStatus.Rejected, Constants.LogAppReject);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication(InputViewModel vm)
        {
            try
            {
                await _appService.CreateAsync(vm.EmailId, User.Identity.Name, vm.EGN, vm.Name, vm.Phone);
                _logger.LogInformation(string.Format(Constants.LogAppCreate, User.Identity.Name, vm.EmailId));

                await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Open);
                _logger.LogInformation(string.Format(Constants.LogEmailOpen, User.Identity.Name, vm.EmailId));

                TempData[Constants.TempDataMsg] = Constants.AppCreateSucc;

                return RedirectToAction(Constants.PageGetOpenEmails, Constants.PageEmail);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }
        private IActionResult ErrorHandle(Exception ex)
        {
            _logger.LogError(ex.Message);

            TempData["globalError"] = Constants.ErrorCatch;

            return View(Constants.PageIndex);
        }       
    }
}