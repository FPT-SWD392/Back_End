using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    [PrimaryKey(nameof(UserId),nameof(ArtId))]
    public class ArtRating : ISqlObject
    {
        [ForeignKey(nameof(UserInfo))]
        [Required]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(ArtInfo))]
        public int ArtId { get; set; }
        [Required]
        public DateTime RatingDate { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating {  get; set; }

        public virtual UserInfo UserInfo { get; set; }
        public virtual ArtInfo ArtInfo { get; set; }
    }
}
