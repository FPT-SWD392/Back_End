using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IFollowRepository
    {
        public Task CreateNewFollow(Follow follow);
        public Task DeleteFollow(Follow follow);
        public Task UpdateFollow(Follow follow);
        public Task<List<Follow>> GetAllUserFollows(int userId);
        public Task<List<Follow>> GetArtistFollowers(int creatorId);
        public Task<Follow?> GetFollow(int userId, int creatorId);
        public ISqlFluentRepository<Follow> Query();
        public Task<long> CountArtistFollows(int creatorId);
    }
}
