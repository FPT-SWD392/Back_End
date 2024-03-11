using BusinessObject.SqlObject;
using Repository.Interface;
using Utils.PasswordHasher;

namespace Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserInfoRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public AuthenticationService(IUserInfoRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserInfo?> Login(string email, string password)
        {
            UserInfo? user = await _userRepository
                .GetUserByEmail(email);
            if (user == null || false == _passwordHasher.VerifyPassword(password, user.PasswordHash)) return null;

            return user;
        }
    }
}
