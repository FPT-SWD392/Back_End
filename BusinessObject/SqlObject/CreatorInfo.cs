using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    public class CreatorInfo : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int CreatorId { get; set; }
        [Required]
        public required string Bio {  get; set; }
        [Required]
        public required DateTime BecomeArtistDate { get; set; }
        [Required]
        public required AcceptCommissionStatus AcceptCommissionStatus {  get; set; }
        [Required]
        public required string ContactInfo { get; set; }    

        public virtual UserInfo? UserInfo { get; set; }
        public virtual ICollection<Follow>? Follows { get; set; }
        public virtual ICollection<Commission>? Commissions { get; set; }
        public virtual ICollection<ArtInfo>? ArtInfos { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
