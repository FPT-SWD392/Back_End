using BusinessObject.SqlObject;

namespace Repository.Interface
{
    public interface ICreatorInfoRepository
    {
        public Task CreateNewCreatorInfo(CreatorInfo creatorInfo);
        public Task UpdateCreatorInfo(CreatorInfo creatorInfo);
        public Task<List<CreatorInfo>> GetAllCreatorInfo();
        public Task<CreatorInfo?> GetCreatorInfo(int creatorId);
    }
}
