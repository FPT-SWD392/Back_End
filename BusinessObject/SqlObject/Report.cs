using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SqlObject
{
    public class Report : ISqlObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
        [Required]
        public required string ReportReason { get; set; }
        [Required]
        public required string ReportDescription { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }
        [ForeignKey(nameof(Reporter))]
        [Required]
        public int ReporterId { get; set; }
        [Required]
        public int ReportedObjectId { get; set; }
        [Required]
        public ReportedObjectType ReportedObjectType { get; set; }

        public virtual UserInfo Reporter {  get; set; }
    }
}