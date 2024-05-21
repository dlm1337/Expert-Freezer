namespace ExpertFreezerAPI.Models
{
    public class ExpertFreezerProfileDTO
    {
        public long? Id { get; set; }
        public string? Company { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public bool IsComplete { get; set; }
    }
}