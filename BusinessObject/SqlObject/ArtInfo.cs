using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.SqlObject
{
    public class ArtInfo : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtId { get; set; }
        [Required]
        public required string ArtName { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        public DateTime UpdateDate {  get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [ForeignKey(nameof(CreatorInfo))]
        public int CreatorId { get; set; }
        [Required]
        public required ArtStatus Status { get; set; }

        public virtual CreatorInfo CreatorInfo { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<ArtRating> ArtRatings { get; set; }
        public virtual ICollection<ArtTag> ArtTags { get; set; }
    }
}
