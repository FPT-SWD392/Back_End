using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ArtworkListDTO
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public List<ArtworkPreviewDTO> ArtworkPreviews { get; set;}
    }
}
