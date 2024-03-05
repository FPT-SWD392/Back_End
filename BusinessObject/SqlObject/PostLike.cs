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
    [PrimaryKey(nameof(UserId),nameof(PostId))]
    public class PostLike : ISqlObject
    {
        [Required]
        [ForeignKey(nameof(UserInfo))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        [Required]
        public DateTime LikedDate { get; set; }

        public virtual UserInfo? UserInfo { get; set; }
        public virtual Post? Post { get; set; }
    }
}
