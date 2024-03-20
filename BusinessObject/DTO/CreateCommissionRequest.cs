namespace BusinessObject.DTO
{
    public class CreateCommissionRequest
    {
        public required DateTime Deadline { get; set; }
        public required double Price { get; set; }
        public required int CreatorId { get; set; }
        public required string Description { get; set; }
    }
}
