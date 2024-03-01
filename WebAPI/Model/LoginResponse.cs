using BusinessObject.SqlObject;

namespace WebAPI.Model
{
    public class LoginResponse
    {
        public required string Token {  get; set; }
        public required string UserName { get; set; }
        public required UserRole UserRole { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
