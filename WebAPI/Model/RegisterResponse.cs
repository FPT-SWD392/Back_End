namespace WebAPI.Model
{
    public class RegisterResponse
    {
        public string? EmailInvalid { get; set; }
        public string? PasswordInvalid { get; set; }
        public string? PhoneNumberInvalid { get; set; }
        public string? NickNameInvalid { get; set; }
    }
}
