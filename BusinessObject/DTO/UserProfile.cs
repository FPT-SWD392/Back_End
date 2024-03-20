namespace BusinessObject.DTO
{
    public class UserProfile
    {

        public required int UserId { get; set; }
        public required string FullName { get; set; }
        public required string Location { get; set; }
        public required string PhoneNumber { get; set; }
        public required string NickName { get; set; }
        public required string Email { get; set; }
        public required double Balance { get; set; }
        public string? Bio { get; set; }
        public string? ContactInfo { get; set; }
        
    }
}
