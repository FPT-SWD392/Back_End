using BusinessObject;

namespace BusinessObject.DTO
{
    public class TransactionResponse
    {
        public int UserId { get; set; }
        //public int TransactionId { get; set; }
        public required int TransactionId {get; set;}
        public string Note { get; set; }
        public required double Amount { get; set; }
        public required DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool IsSuccess { get; set; }
    }
}
