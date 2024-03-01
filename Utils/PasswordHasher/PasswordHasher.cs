using BC = BCrypt.Net.BCrypt;
namespace Utils.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly int _workFactor = 13;
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));
            return BC.EnhancedHashPassword(password, _workFactor);
        }
        public bool VerifyPassword(string password, string hashed_password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(hashed_password))
                throw new ArgumentNullException(nameof(hashed_password));
            return BC.EnhancedVerify(password, hashed_password);
        }
    }
}
