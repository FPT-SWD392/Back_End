using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITagService
    {
        Task<Tag?> GetTag(int tagId);
        Task<List<Tag>> GetTags();
        Task AddTag(Tag tag);
        Task RemoveTag(Tag tag);
        Task UpdateTag(Tag tag);
    }
}
