using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;

namespace EasyTicket.Server.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserLoginDto request);
    }
}
