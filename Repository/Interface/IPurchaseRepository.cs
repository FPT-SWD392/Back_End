using BusinessObject.SqlObject;

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
    }
}
