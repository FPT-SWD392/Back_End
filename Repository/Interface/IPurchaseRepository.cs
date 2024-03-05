using BusinessObject.SqlObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPurchaseRepository
    {
        public Task CreateNewPurchase(Purchase purchase);
        public Task DeletePurchase(Purchase purchase);
        public Task UpdatePurchase(Purchase purchase);
        public Task<Purchase?> GetPurchaseById(int purchaseId);
        public Task<List<Purchase>> GetAllUserPurchases(int userId);
        public Task<List<Purchase>> GetAllArtPurchases(int artId);
        public ISqlFluentRepository<Purchase> Query();
    }
}
