using EasyTicket.Server.Data;
using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyTicket.Server.Services.Auth
    {
    public class AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IAuthService
        {
        public async Task<UserLoginResponseDTO?> LoginAsync(UserLoginDTO request)
            {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return null;
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password) == PasswordVerificationResult.Failed)
                return null;
            var response = new UserLoginResponseDTO(
                Id: user.Id, Name: user.Name, Role : user.Role, Token: CreateToken(user)
                );


            return response;
            }

        public async Task<User?> RegisterAsync(UserDTO request)
            {
            var usr = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usr != null)
                return null;
            User user = new()
                {
                Email = request.Email,
                Name = request.Name,
                Role = request.Role,
                HashedPassword = ""

                };
            var passwordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.HashedPassword = passwordHash;
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
            }

        private string CreateToken(User user)
            {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds

                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }

        public  CurrentUserDTO? GetCurrentUser()
            {
            if (httpContextAccessor.HttpContext is null)
                return null;
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var role = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (userId == null || userName == null || role == null)
                return null;

            CurrentUserDTO user = new CurrentUserDTO(
                Id: userId,
                name: userName,
                Role: role
                );
                

               
            return user;
            }
    } 
}

    
