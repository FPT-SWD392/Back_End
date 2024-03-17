using BusinessObject;

namespace BusinessObject.DTO
{
    public class TransactionRequest
    {
        public string Note { get; set; }
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
