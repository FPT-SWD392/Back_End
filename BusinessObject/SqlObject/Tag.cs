using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.SqlObject
{
    public class Tag : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        [Required]
        [StringLength(20)]
        public required string TagName { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
