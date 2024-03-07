using BusinessObject;
using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserInfoService
    {
        Task<UserInfo?> GetUserByUserId(int userId);
        Task<UserInfo?> GetUserByCreatorId(int creatorId);
    }
}
