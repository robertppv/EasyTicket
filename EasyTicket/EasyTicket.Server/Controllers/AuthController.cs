using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;
using EasyTicket.Server.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EasyTicket.Server.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
        {
        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(UserLoginDTO request)
            {
            var res = await authService.LoginAsync(request);

            if (res is null)
                return BadRequest();
            return Ok(res);
            }


        [HttpPost("register")]

        public async Task<ActionResult<User>> Register(UserDTO request)
            {
            var user = await authService.RegisterAsync(request);
            if (user == null)
                return BadRequest();
            return Ok(user);
            }


        [HttpGet("get/user")]
        [Authorize]
        public async Task<ActionResult> GetCurrentUser()
            {
            var user=authService.GetCurrentUser();
            if (user == null)
                return BadRequest();
            return Ok(user);
            }
        
        }

    }
