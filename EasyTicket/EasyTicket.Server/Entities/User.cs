namespace EasyTicket.Server.Entities
{
    public class User
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString();
        required
        public string Name { get; set; } = string.Empty;
        required
        public string Email { get; set; } = string.Empty; 
        required
        public string HashedPassword {  get; set; } = string.Empty;
        required
        public string Role {  get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; }=new List<Ticket>();

    }
}
