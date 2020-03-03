using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Services.Contracts;
using EMS.Services.dto_Models;
using EMS.Services.Factories.Contracts;
using EMS.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Services
{
    public class UserService : IUserService
    {
        private readonly SystemDataContext _context;
        private readonly UserManager<UserDomain> _userManager;
        private readonly IUserFactory _factory;

        public UserService(SystemDataContext context, UserManager<UserDomain> userManager, IUserFactory factory)
        {
            _context = context;
            _userManager = userManager;
            _factory = factory;
        }

        public async Task ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await GetUser(username);
            var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, currentPassword);

            if (!passwordIsCorrect)
            {
                throw new ArgumentException(Constants.UserInvalidPass);
            }
            else
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (result.Succeeded)
                {
                    user.IsPasswordChanged = true;
                    var claim = _context.UserClaims.FirstOrDefault(userclaim => userclaim.ClaimType == "IsPasswordChanged" && userclaim.UserId == user.Id);
                    claim.ClaimValue = "True";
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException(Constants.UserInvalidPass);
                }
            }
        }

        public async Task CreateAsync(string username, string password, string role)
        {
            if (_context.Users.Any(user => user.UserName == username))
            {                
                throw new ArgumentException(string.Format(Constants.UserExists, username));
            }
            else
            {
                await _factory.CreateUser(username, password, role);
            }
        }

        public async Task<UserDto> GetUserAsync(string username)
        {
            var user = await GetUser(username);
            return user.MapToDtoModel();
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await GetUser(username);
            return user.Id;
        }

        private async Task<UserDomain> GetUser(string username) => await _userManager.FindByNameAsync(username);
    }
}
