namespace WebAPI.Model
{
    public class RegisterRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string Location { get; set; }
        public required string PhoneNumber { get; set; }
        public required string NickName { get; set; }
    }
}