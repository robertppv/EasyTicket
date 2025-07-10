namespace EasyTicket.Server.Models
    {
    public record class UserLoginResponseDTO(
        string Name,
        string Role,
        string Id,
        string Token
        );
       
    }
