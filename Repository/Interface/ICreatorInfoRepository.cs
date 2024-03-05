using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICreatorInfoRepository
    {
        public Task CreateNewCreatorInfo(CreatorInfo creatorInfo);
        public Task UpdateCreatorInfo(CreatorInfo creatorInfo);
        public Task<List<CreatorInfo>> GetAllCreatorInfo();
        public Task<CreatorInfo?> GetCreatorInfo(int creatorId);
        public ISqlFluentRepository<CreatorInfo> Query();
    }
}
