using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPurchaseService
    {
        public Task<bool> PurchaseWithBalance(int userId, int artId);
        public Task<bool> PurchaseWithExternalParty(int userId, int artId);
    }
}
