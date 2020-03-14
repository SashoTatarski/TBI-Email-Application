using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.WebProject.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.WebProject.Controllers
{
    [Authorize(Roles = "manager, operator")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<UserDomain> _signInManager;
        private readonly ILogger<EmailController> _logger;
        private readonly UserManager<UserDomain> _userManager;

        public UserController(IUserService userService, SignInManager<UserDomain> signInManager, 
            ILogger<EmailController> logger, UserManager<UserDomain> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register(string error = null)
        {
            try
            {
                var listRoles = new List<SelectListItem> {
                    new SelectListItem { Text = Constants.SelListTextManager, Value = Constants.SelListValueManager },
                    new SelectListItem { Text = Constants.SelListTextOperator, Value = Constants.SelListValueOperator }
                };

                var viewModel = new RegisterUserViewModel
                {
                    Roles = listRoles,
                    Error = error
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ErrorHandle(ex);

                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.CreateAsync(vm.Email, vm.Password, vm.Role);
                    _logger.LogInformation(string.Format(Constants.LogUserCreate, User.Identity.Name, vm.Role, vm.Email));

                    TempData[Constants.TempDataMsg] = Constants.UserCreateSucc;
                }
                else
                {
                    return RedirectToAction("Register", "User", new { error = Constants.InvalidData });
                }
            }
            catch (Exception ex)
            {
                ErrorHandle(ex);
            }

            return RedirectToAction(Constants.PageRegister, Constants.PageUser);
        }

        private void ErrorHandle(Exception ex)
        {
            TempData["globalError"] = Constants.ErrorCatch;

            _logger.LogError(ex.Message);
        }

        [HttpGet]
        public IActionResult ChangePassword(string error = null)
        {
            TempData["error"] = error;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("ChangePassword", "User", new { error = Constants.InvalidData });
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, vm.CurrentPassword);

                    if (!isPasswordCorrect)
                    {
                        return RedirectToAction("ChangePassword", "User", new { error = Constants.UnknownPass });
                    }
                    else
                    {
                        await ChangeNewPassword(vm, user);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandle(ex);
            }

            return LocalRedirect(Constants.ChangePassRedirect);
        }

        private async Task ChangeNewPassword(ChangePasswordViewModel vm, UserDomain user)
        {
            await _userService.ChangePasswordAsync(user.UserName, vm.CurrentPassword, vm.Password);
            _logger.LogInformation(string.Format(Constants.LogUserPassChange, User.Identity.Name));

            TempData[Constants.TempDataMsg] = Constants.UserPassChangeSucc;

            await _signInManager.SignOutAsync();

            var userName = User.Identity.Name;
            _logger.LogInformation(string.Format(Constants.LogUserSignOut, userName));
        }
    }
}