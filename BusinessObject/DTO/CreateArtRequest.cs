using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTO
{
    public class CreateArtRequest
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public required double Price { get; set; }
        [Required]
        public required List<int> Tags { get; set; }
        [Required]
        public required IFormFile ImageFile { get; set; }
    }
}
