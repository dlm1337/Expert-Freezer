namespace ExpertFreezerAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}