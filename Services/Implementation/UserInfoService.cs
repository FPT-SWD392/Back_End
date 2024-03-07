using BusinessObject;
using BusinessObject.SqlObject;
using Repository.Implementation;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userRepository;
        private readonly ICreatorInfoRepository _creatorInfoRepository;
        public UserInfoService(IUserInfoRepository userInfoRepository, ICreatorInfoRepository creatorInfoRepository)
        {
            _userRepository = userInfoRepository;
            _creatorInfoRepository = creatorInfoRepository;
        }

        public Task<UserInfo?> GetUserByUserId(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public async Task<UserInfo?> GetUserByCreatorId(int creatorId)
        {
            CreatorInfo creator = await _creatorInfoRepository.GetCreatorInfo(creatorId);
            return creator.UserInfo;
        }
    }
}
