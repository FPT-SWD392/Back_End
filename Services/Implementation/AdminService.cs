using JwtTokenAuthorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.PasswordHasher;

namespace Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly string? _sysAdminUsername;
        private readonly string? _sysAdminPassword;
        private readonly ITokenHelper _tokenHelper;

        private readonly IPasswordHasher _passwordHasher;
        public AdminService(IConfiguration configuration, IPasswordHasher passwordHasher, ITokenHelper tokenHelper)
        {
            _sysAdminUsername = configuration["SYSADMIN:USERNAME"];
            _sysAdminPassword = configuration["SYSADMIN:PASSWORD"];
            _passwordHasher = passwordHasher;
            _tokenHelper = tokenHelper;
        }

        public async Task<string> Login(string username, string password)
        {
            if (_sysAdminUsername == username && _passwordHasher.VerifyPassword(password,_sysAdminPassword))
            {
                return _tokenHelper.GenerateSysAdminToken();
            }
            return "";
        }
    }
}
