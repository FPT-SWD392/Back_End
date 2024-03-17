using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IArtService
    {
        public Task<List<ArtInfo>> GetArtList(string? searchValue, List<int> tagIds, int pageNumber);
    }
}
