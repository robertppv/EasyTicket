using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;

namespace EasyTicket.Server.Services.Auth
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<string?> LoginAsync(UserLoginDTO request);
    }
}
