namespace BusinessObject.DTO
{
    public class RegisterResponse
    {
        public string? ErrorEmail { get; set; }
        public string? ErrorPassword { get; set; }
        public string? ErrorPhoneNumber { get; set; }
        public string? ErrorNickName { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
