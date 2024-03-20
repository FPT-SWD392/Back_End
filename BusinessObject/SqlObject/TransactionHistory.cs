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
    public class TransactionHistory : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Required]
        [ForeignKey(nameof(UserInfo))]
        public int UserId { get; set; }

        [Required]  
        public required string Note { get; set; }

        [Required]
        [Range(double.Epsilon,double.MaxValue)]
        public double Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public required TransactionType TransactionType { get; set; }
        public bool IsSuccess { get; set; } = true;

        public virtual UserInfo UserInfo { get; set; }
    }
}
