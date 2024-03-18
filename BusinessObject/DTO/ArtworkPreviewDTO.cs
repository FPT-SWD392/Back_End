using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class ArtworkPreviewDTO
    {
        public int ArtId { get; set; }
        public required string ArtName { get; set; }
        public double Price { get; set; }
        public int CreatorId { get; set; }
        public required string CreatorNickName { get; set; }
        public required string CreatorProfilePicture { get; set; }

        public List<int> Tags { get; set; }

        public int RatingCount { get; set; }
        public double AverageRating {  get; set; }

        public required ArtStatus Status { get; set; }
    }
}
