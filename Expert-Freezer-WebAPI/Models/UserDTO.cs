namespace ExpertFreezerAPI.Models
{
    public class UserDTO
    {
        public long Id { get; set; }
        public required string Username { get; set; } 
        public string? Email { get; set; }
    }
}