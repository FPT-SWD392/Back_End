using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    public class Commission : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommissionId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime Deadline {  get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public CommissionStatus CommissionStatus { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [ForeignKey(nameof(UserInfo))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(CreatorInfo))]
        public int CreatorId { get; set; }
        public string? ImageId { get; set; }
        public int? Rating {  get; set; }
        public DateTime? RatingDate { get; set; }
        public string? Review { get; set; }
        
        public virtual UserInfo UserInfo { get; set; }
        public virtual CreatorInfo CreatorInfo { get; set; }
    }
}
