using EasyTicket.Server.Data;
using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyTicket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserDTO user)
        {
            var urt = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (urt != null)
            {
                return BadRequest();
            }
            User newUser = new()
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                HashedPassword = user.Password
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }

        [HttpPut]

        public async Task<ActionResult<User>> PutUser(User user)
        {
            var u = await _context.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            if (u == null)
                return NotFound();
            u.Email = user.Email;
            u.Name = user.Name;
            u.Role = user.Role;

            await _context.SaveChangesAsync();
            return Ok(u);

        }


        [HttpPatch]
        public async Task<ActionResult> UpdatePassword(string Id, string pass, string newPass)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Id == Id);
            if (user == null)
                return NotFound();

            if (user.HashedPassword != pass)
                return BadRequest("Wrong password");

            user.HashedPassword = newPass;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
