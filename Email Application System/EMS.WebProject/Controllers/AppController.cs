﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Data.Enums;
using EMS.Data.Seed;
using EMS.Services.Contracts;
using EMS.WebProject.Mappers;
using EMS.WebProject.Models.Applications;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebProject.Controllers
{
    public class AppController : Controller
    {
        private readonly IApplicationService _appService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;


        public AppController(IApplicationService appService, IUserService userService, IEmailService emailService)
        {
            _appService = appService;
            _userService = userService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Preview(string id)
        {
            var appByEmailId =  await _appService.GetAppByMailIdAsync(id);

            var vm = appByEmailId.MapToViewModelPreview(id);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAppStatus(AppPreviewViewModel vm)
        {
            await _appService.ChangeStatusAsync(vm.Id, ApplicationStatus.Approved);

            await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Closed);

            TempData["message"] = Constants.SuccAppValid;

            return RedirectToAction("Index", "Email");
        }

        public async Task<IActionResult> MarkInvalid(AppPreviewViewModel vm)
        {
            await _appService.ChangeStatusAsync(vm.Id, ApplicationStatus.Rejected);

            await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Closed);

            TempData["message"] = Constants.SuccAppInvalid;

            return RedirectToAction("Index", "Email");
        }

        public async Task<IActionResult> MarkOpen(InputViewModel vm)
        {            
            var user = await _userService.FindUserAsync(User.Identity.Name);

            await _appService.CreateApplicationAsync(vm.EmailId, user.Id, vm.EGN, vm.Name, vm.Phone);

            await _emailService.ChangeStatusAsync(vm.EmailId, EmailStatus.Open);

            TempData["message"] = Constants.SuccAppCreate;

            return RedirectToAction("Index", "Email");
        }
    }
}