using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRegisterService
    {
        public Task<List<string>?> Register(UserInfo userInfo, string password);
    }
}
