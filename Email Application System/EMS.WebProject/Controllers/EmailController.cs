using EMS.Data;
using EMS.Data.Enums;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Emails;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Policy = Constants.AuthPolicy)]
    [Authorize(Roles = "manager, operator")]

    public class EmailController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailService emailService, IApplicationService appService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _appService = appService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                var allEmails = await _emailService.GetAllEmailsAsync();
                allEmails = SortIndexAndClosedEmails(sortOrder, allEmails);

                var vm = CreateViewModel(allEmails, Constants.TabAll);

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }
        private List<EmailDto> SortIndexAndClosedEmails(string sortOrder, List<EmailDto> emails)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            switch (sortOrder)
            {
                case "Date":
                    emails = emails.OrderBy(mail => mail.Received).ToList();
                    break;
                case "date_desc":
                    emails = emails.OrderByDescending(mail => mail.Received).ToList();
                    break;
                default:
                    emails = emails.OrderByDescending(mail => mail.Received).ToList();
                    break;
            }

            return emails;
        }

        private static AllEmailsViewModel CreateViewModel(List<EmailDto> emails, string activeTab)
        {
            return new AllEmailsViewModel
            {
                AllEmails = emails.Select(x => x.MapToViewModel()).ToList(),
                ActiveTab = activeTab
            };
        }

        [HttpGet("ClosedEmails")]
        public async Task<IActionResult> GetClosedEmails(string sortOrder)
        {
            try
            {
                var closedEmails = await _emailService.GetEmailsAsync(EmailStatus.Closed);
                closedEmails = SortIndexAndClosedEmails(sortOrder, closedEmails);

                var vm = CreateViewModel(closedEmails, Constants.TabClosed);
               
                foreach (var email in vm.AllEmails)
                {
                    email.OperatorUsername = await _appService.GetOperatorUsernameAsync(email.Id);
                    email.ApplicationStatus = await _appService.GetStatusAsync(email.Id);
                    email.ApplicationId = await _appService.GetAppIdByMailIdAsync(email.Id);
                }

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }

        }

        [HttpGet("NewEmails")]
        public async Task<IActionResult> GetNewEmails(string sortOrder)
        {
            try
            {
                var newEmails = await _emailService.GetEmailsAsync(EmailStatus.New);
                newEmails = SortNewAndOpenEmails(sortOrder, newEmails);

                var vm = CreateViewModel(newEmails, Constants.TabNew);
               
                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        private List<EmailDto> SortNewAndOpenEmails(string sortOrder, List<EmailDto> newEmails)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["SinceStatus"] = sortOrder == "SinceStatus_Date" ? "sinceStatus_desc" : "SinceStatus_Date";
            switch (sortOrder)
            {
                case "Date":
                    newEmails = newEmails.OrderBy(mail => mail.Received).ToList();
                    break;
                case "date_desc":
                    newEmails = newEmails.OrderByDescending(mail => mail.Received).ToList();
                    break;
                case "SinceStatus_Date":
                    newEmails = newEmails.OrderBy(mail => mail.ToCurrentStatus).ToList();
                    break;
                case "sinceStatus_desc":
                    newEmails = newEmails.OrderByDescending(mail => mail.ToCurrentStatus).ToList();
                    break;
                default:
                    newEmails = newEmails.OrderByDescending(mail => mail.Received).ToList();
                    break;
            }

            return newEmails;
        }

        [HttpGet("OpenEmails")]
        public async Task<IActionResult> GetOpenEmails(string sortOrder)
        {
            try
            {
                var openEmails = await _emailService.GetEmailsAsync(EmailStatus.Open);
                openEmails = SortNewAndOpenEmails(sortOrder, openEmails);               

                var apps = await _appService.GetOpenAppsAsync();

                var vm = CreateViewModel(openEmails, Constants.TabOpen);               

                foreach (var emailVM in vm.AllEmails)
                {
                    emailVM.OperatorUsername = await _appService.GetOperatorUsernameAsync(emailVM.Id);
                    emailVM.ApplicationId = await _appService.GetAppIdByMailIdAsync(emailVM.Id);
                }

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }

        }        

        [HttpGet]
        public async Task<IActionResult> MarkInvalid(string id)
        {
            try
            {
                await _emailService.ChangeStatusAsync(id, EmailStatus.Invalid);

                _logger.LogInformation(string.Format(Constants.LogEmailInvalid, User.Identity.Name, id));

                TempData[Constants.TempDataMsg] = Constants.EmailInvalidSucc;

                var allEmails = new List<EmailDto>();
                allEmails = await _emailService.GetAllEmailsAsync();

                var vm = CreateViewModel(allEmails, Constants.TabAll);

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MarkNotReviewed(string id)
        {
            try
            {
                await _emailService.ChangeStatusAsync(id, EmailStatus.NotReviewed);

                _logger.LogInformation(string.Format(Constants.LogEmailNotReviewd, User.Identity.Name, id));
                TempData[Constants.TempDataMsg] = Constants.EmailNotReviewedSucc;

                var allEmails = await _emailService.GetAllEmailsAsync();

                var vm = CreateViewModel(allEmails, Constants.TabAll);

                return View(Constants.PageIndex, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MarkNew(string id)
        {
            try
            {
                await _emailService.ChangeStatusAsync(id, EmailStatus.New);

                _logger.LogInformation(string.Format(Constants.LogEmailNew, User.Identity.Name, id));

                TempData[Constants.TempDataMsg] = Constants.EmailNewSucc;

                var body = await _emailService.GetBodyByDbAsync(id);

                var email = await _emailService.GetSingleEmailAsync(id);

                var attachmentsViewModel = MapAttachments(email);

                var vm = email.MapToViewModelPreview(this.SanitizeContent(body), attachmentsViewModel);
                vm.InputViewModel.EmailId = id;

                return View(Constants.PageOpen, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        private static List<AttachmentViewModel> MapAttachments(EmailDto email)
        {
            var attachmentsVM = new List<AttachmentViewModel>();
            if (email.Attachments.Count != 0)
            {
                foreach (var attachments in email.Attachments)
                {
                    attachmentsVM.Add(attachments.MapToViewModel());
                }
            }

            return attachmentsVM;
        }

        [HttpGet]
        public async Task<IActionResult> MarkOpen(string id)
        {
            try
            {
                var body = await _emailService.GetBodyByDbAsync(id);
                var sanitizedBody = this.SanitizeContent(body);

                var email = await _emailService.GetSingleEmailAsync(id);

                var attachmentsViewModel = MapAttachments(email);

                var vm = email.MapToViewModelPreview(sanitizedBody, attachmentsViewModel);
                vm.InputViewModel.EmailId = id;

                return View(Constants.PageOpen, vm);
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailBody(string id)
        {
            try
            {
                var body = await _emailService.GetBodyByDbAsync(id);

                if (body == Constants.NoBody)
                {
                    var gmailId = await _emailService.GetGmailIdAsync(id);
                    body = await _emailService.GetBodyByGmailAsync(gmailId);
                }

                return Json(this.SanitizeContent(body));
            }
            catch (Exception ex)
            {
                return ErrorHandle(ex);
            }
        }
        private string SanitizeContent(string content)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedContent = sanitizer.Sanitize(content);

            return (sanitizedContent == "") ? Constants.BlockedContent : sanitizedContent;
        }

        private IActionResult ErrorHandle(Exception ex)
        {
            _logger.LogError(ex.Message);

            TempData["globalError"] = Constants.ErrorCatch;

            return View(Constants.PageIndex);
        }
    }
}