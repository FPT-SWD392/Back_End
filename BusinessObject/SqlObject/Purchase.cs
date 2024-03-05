using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessObject.SqlObject
{
    public class Purchase :ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }
        [Required]
        [Range(double.Epsilon,double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [ForeignKey(nameof(UserInfo))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(ArtInfo))]
        public int ArtId { get; set; }
        [Required]
        
        public virtual UserInfo? UserInfo { get; set; }
        public virtual ArtInfo? ArtInfo { get; set;}
    }
}
