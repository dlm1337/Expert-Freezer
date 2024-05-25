namespace ExpertFreezerAPI.Models
{
    public class RegistrationDTO
    {
        public long Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}