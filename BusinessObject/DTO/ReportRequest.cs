using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ReportRequest
    {
        public required string Reason { get; set; }
        public required string Description { get; set; }
        public required int ReporterId { get; set; }
        public required ReportedObjectType ReportedObjectType { get; set; }
        public required int ReportedObjectId { get; set; }
        public required DateTime ReportDate { get; set; }
    }
}
