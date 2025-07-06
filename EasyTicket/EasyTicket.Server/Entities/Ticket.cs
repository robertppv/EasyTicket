namespace EasyTicket.Server.Entities
{
    public class Ticket
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Title { get; set; }=string.Empty;
        public required string Description { get; set; }=string.Empty ;
        public DateTime Created { get; set; }=DateTime.Now;
        public string Status { get; set; } = "IN PROGESS";

        public string? UserID { get; set; }
        public User? User { get; set; }
    }
}
