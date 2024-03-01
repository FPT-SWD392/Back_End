using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthenticationService
    {
        Task<User?> Login(string email, string password);
        Task CreateUser();
    }
}
