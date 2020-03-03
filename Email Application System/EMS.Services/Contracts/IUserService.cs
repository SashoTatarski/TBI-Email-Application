using EMS.Services.dto_Models;
using System.Threading.Tasks;

namespace EMS.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string usermane);
        Task CreateAsync(string username, string password, string role);
        Task ChangePasswordAsync(string username, string currentPassword, string newPassword);
        Task<string> GetUserIdAsync(string username);
    }
}
