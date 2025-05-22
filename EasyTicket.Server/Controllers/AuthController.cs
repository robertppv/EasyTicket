using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;
using EasyTicket.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
           var user = await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            var token= await authService.LoginAsync(request);

            if (token == null)
            {
                return BadRequest("Invalid credentials");
            }
            return Ok(token);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult endpoint()
        {
            return Ok("You are authorized to access this endpoint.");
        }
       
    }
}
