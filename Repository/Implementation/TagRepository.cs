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
    public class TagRepository : ITagRepository
    {
        IGenericDao<Tag> _dao;
        public TagRepository(IDaoFactory daoFactory)
        {
            _dao = daoFactory.CreateDao<Tag>();
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _dao
                .Query()
                .ToListAsync();
        }

        public async Task<Tag?> GetTagById(int tagId)
        {
            return await _dao
                .Query()
                .Where(x => x.TagId == tagId)
                .SingleOrDefaultAsync();
        }
        public async Task AddTag(Tag tag)
        {
            await _dao.CreateAsync(tag);
        }
        public async Task DeleteTag(Tag tag)
        {
            await _dao.DeleteAsync(tag);
        }
        public async Task UpdateTag(Tag tag)
        {
            await _dao.UpdateAsync(tag);
        }
    }
}
