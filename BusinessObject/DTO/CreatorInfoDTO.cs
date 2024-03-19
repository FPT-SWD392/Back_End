using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class CreatorInfoDTO
    {
        public required string Bio { get; set; }
        public required string ContactInfo { get; set; }
    }
}
