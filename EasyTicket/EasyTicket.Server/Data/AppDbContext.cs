using EasyTicket.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyTicket.Server.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets {  get; set; }
    }
}
