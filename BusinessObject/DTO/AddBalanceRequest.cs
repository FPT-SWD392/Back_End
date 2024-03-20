using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class AddBalanceRequest
    {
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public int? userId { get; set; }
    }
}
