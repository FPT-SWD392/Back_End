namespace WebAPI.Model
{
    public class UserProfile
    {
        public required string FullName { get; set; }
        public required string Location { get; set; }
        public required string PhoneNumber { get; set; }
        public required string NickName { get; set; }
    }
}
