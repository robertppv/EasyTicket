using EasyTicket.Server.Data;
using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyTicket.Server.Services.Auth
    {
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
        {
        public async Task<string?> LoginAsync(UserLoginDTO request)
            {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return null;
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, request.Password) == PasswordVerificationResult.Failed)
                return null;
            return CreateToken(user);
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
        }
    }
