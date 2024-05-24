
namespace ExpertFreezerAPI.Models
{


    public class ExpertFreezerProfile
    {
        public long? Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfilePic { get; set; } // Base64 since it is just a sample app will be ok. I dont have any money for s3 bucket!
        public List<string>? ExtraPics { get; set; } // Base64 since it is just a sample app will be ok. I dont have any money for s3 bucket!
        public List<string>? ExtraPicsDesc { get; set; }
        public string? CompanyDescription { get; set; }
        public string? Services { get; set; }
        public string? Address { get; set; }
        public string? Pricing { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}