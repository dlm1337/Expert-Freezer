namespace ExpertFreezerAPI.Models
{
    public class ExpertFreezerProfileDTO
    {
        public long? Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfilePic { get; set; }
        public List<string>? ExtraPics { get; set; }
        public List<string>? ExtraPicsDesc { get; set; }
        public string? CompanyDescription { get; set; }
        public string? Services { get; set; }
        public string? Address { get; set; }
        public string? Pricing { get; set; }
        public bool IsComplete { get; set; }
    }
}