using EasyTicket.Server.Data;
using EasyTicket.Server.Entities;
using EasyTicket.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace EasyTicket.Server.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
        {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext dbContext)
            {
            _context = dbContext;
            }


        [HttpGet]

        public async Task<ActionResult<List<Ticket>>> GetAllTickets()
            {
            var tickets = await _context.Tickets.ToListAsync();
            return Ok(tickets);
            }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{userId}/tickets")]

        public async Task<ActionResult<List<TicketDTO>>> GetTicketsByUserId(string userId)
            {
            var user = await _context.Users.FindAsync(userId);
            if (userId == null)
                return NotFound();
            var tickets = await _context.Tickets.Where(u => u.UserID == userId).ToListAsync();

            var ticketDto = tickets.Select(t => new TicketDTO(t.Title, t.Description)).ToList();
            return Ok(ticketDto);
            }


        [HttpGet("{ticketId}")]

        public async Task<ActionResult<Ticket>> GetTicketById(string ticketId)
            {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(u => u.Id == ticketId);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
            }

        [HttpPost("{userId}")]

        public async Task<ActionResult> AddTicketToUser(string userId, TicketDTO ticket)
            {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");
            Ticket t = new()
                {
                Description = ticket.Description,
                Title = ticket.Title,
                UserID = userId
                };

            _context.Tickets.Add(t);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTicketById), new { ticketId = t.Id }, t);

            }
        [Authorize(Roles ="Admin")]
        [HttpPatch("id")]
        public async Task<ActionResult<Ticket>> UpdateTicketStatus(string id, string newStatus)
            {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket is null)
                return NotFound();
            ticket.Status=newStatus;
            await _context.SaveChangesAsync();
            return Ok(ticket);
            }
        

        }
    }
