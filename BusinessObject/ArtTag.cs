using BusinessObject.SqlObject;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    [PrimaryKey(nameof(ArtId),nameof(TagId))]
    public class ArtTag
    {
        [ForeignKey(nameof(ArtInfo))]
        public int ArtId { get; set; }
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }

        public virtual ArtInfo ArtInfo { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
