using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllTags();
        Task<Tag?> GetTagById(int tagId);
    }
}
