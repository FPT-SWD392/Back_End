using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    public class Post : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        [ForeignKey(nameof(CreatorInfo))]
        public int CreatorId { get; set; }
        [Required]
        public PostVisibility Visibility { get; set; }

        public virtual CreatorInfo CreatorInfo { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
