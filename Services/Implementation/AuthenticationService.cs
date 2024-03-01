using BusinessObject.SqlObject;
using Repository;
using System.Security.Cryptography;
using System.Text;
using Utils.PasswordHasher;

namespace Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public AuthenticationService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<User?> Login(string email, string password)
        {
            IEnumerable<User>? users = await _userRepository.GetUsersByConditionAsync(u=>u.Email == email);
            if (null == users || false == users.Any()) return null;

            User user = users.Single();
            if (false == _passwordHasher.VerifyPassword(password, user.PasswordHash)) return null;

            return users.SingleOrDefault();
        }
        public async Task CreateUser()
        {
            string passwordHash = _passwordHasher.HashPassword("dotutoan");
            User user = new()
            {
                UserId = "U-1",
                UserName = "Đỗ Tú Toàn",
                Email = "to@n.do",
                PasswordHash = passwordHash,
                Address=string.Empty,
                CreatedDate = DateTime.Now,
                Introduction=string.Empty,
                PhoneNumber=string.Empty,
                ProfilePictureUrl = "https://cdn.discordapp.com/avatars/591436719576842240/5cdbb5c2679fd91d9750d94b1d06a08f.webp?size=1024",
                Status = AccountStatus.Active
            };
            await _userRepository.AddUserAsync(user);
        }

    }
}
