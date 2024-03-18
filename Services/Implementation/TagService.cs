using BusinessObject.SqlObject;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task AddTag(Tag tag)
        {
            await _tagRepository.AddTag(tag);
        }

        public async Task<Tag?> GetTag(int tagId)
        {
            return await _tagRepository.GetTagById(tagId);
        }

        public async Task<List<Tag>> GetTags()
        {
            return await _tagRepository.GetAllTags();
        }

        public async Task RemoveTag(Tag tag)
        {
            await _tagRepository.DeleteTag(tag);
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagRepository.AddTag(tag);
        }
    }
}
