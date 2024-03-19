namespace BusinessObject.DTO
{
    public class UserProfile
    {
        public required string FullName { get; set; }
        public required string Location { get; set; }
        public required string PhoneNumber { get; set; }
        public required string NickName { get; set; }
        public string? Bio { get; set; }
        public string? ContactInfo { get; set; }
    }
}
