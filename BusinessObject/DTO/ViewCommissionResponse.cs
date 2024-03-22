using BusinessObject;

namespace BusinessObject.DTO
{
    public class ViewCommissionResponse
    {
        public required int CommisionId { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime Deadline { get; set; }
        public required double Price { get; set; }
        public required CommissionStatus CommissionStatus { get; set; }
        public string? UserName { get; set; }
        public string? ArtistName { get; set; }
        public int? ImageId { get; set; }
        public int? Rating { get; set; }
        public string? Review { get; set; }
    }
}
