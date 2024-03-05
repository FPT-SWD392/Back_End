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
    [PrimaryKey(nameof(UserId),nameof(CreatorId))]
    public class Follow : ISqlObject
    {
        [Required]
        [ForeignKey(nameof(UserInfo))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(CreatorInfo))]
        public int CreatorId { get; set; }
        [Required]
        public DateTime FollowDate { get; set; }

        public virtual UserInfo? UserInfo { get; set; }
        public virtual CreatorInfo? CreatorInfo { get; set; }
    }
}
