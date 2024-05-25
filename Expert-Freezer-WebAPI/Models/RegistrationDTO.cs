namespace ExpertFreezerAPI.Models
{
    public class RegistrationDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}