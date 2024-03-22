using BusinessObject.SqlObject;
using DataAccessObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class ArtTagRepository : IArtTagRepository
    {
        private readonly IGenericDao<ArtTag> _dao;
        public ArtTagRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<ArtTag>();
        }
        public async Task CreateArtTag(ArtTag artTag)
        {
            await _dao.CreateAsync(artTag);
        }
    }
}
