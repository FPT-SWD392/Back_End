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
        [Required]
        [ForeignKey(nameof(Reporter))]
        public int ReporterId { get; set; }
        [ForeignKey(nameof(ReportedArtInfo))]
        public int? ReportedArtId { get; set; }
        [ForeignKey(nameof(ReportedCreatorInfo))]
        public int? ReportedCreatorId { get; set; }
        [ForeignKey(nameof(ReportedPost))]
        public int? ReportedPostId { get; set; }
        [ForeignKey(nameof(ReportedCommission))]
        public int? ReportedCommissionId { get; set; }
        public ReportedObjectType ReportedObjectType { get; set; }

        public virtual UserInfo? Reporter {  get; set; }

        public virtual ArtInfo? ReportedArtInfo { get; set; }
        public virtual CreatorInfo? ReportedCreatorInfo { get; set; }
        public virtual Post? ReportedPost { get; set; }
        public virtual Commission? ReportedCommission { get; set; }
    }
}