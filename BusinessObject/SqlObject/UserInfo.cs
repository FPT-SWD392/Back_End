using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    public class UserInfo : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {  get; set; }
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string PasswordHash { get; set; }
        [Required]
        public required AccountStatus Status { get; set; }
        [ForeignKey(nameof(CreatorInfo))]
        public int? CreatorId { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string Location { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        [Required]
        public required string NickName { get; set; }
        [Required]
        public required DateTime JoinDate { get; set; }
        [Required]
        public required double Balance { get; set; }

        public virtual CreatorInfo CreatorInfo { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistory { get; set; }
        public virtual ICollection<ArtRating> ArtRatings { get; set; }
        public virtual ICollection<Commission> Commissions { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
    }
}
