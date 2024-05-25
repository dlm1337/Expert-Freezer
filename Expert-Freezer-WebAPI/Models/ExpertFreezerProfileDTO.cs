namespace ExpertFreezerAPI.Models
{
    public class ExpertFreezerProfileDTO
    {
        public long? Id { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfilePic { get; set; }
        public List<string>? ExtraPics { get; set; }
        public List<string>? ExtraPicsDesc { get; set; }
        public string? CompanyDescription { get; set; }
        public string? Services { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Pricing { get; set; }
    }
}