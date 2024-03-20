using BusinessObject.SqlObject;
using Repository.Interface;

namespace Services.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IArtInfoRepository _artInfoRepository;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository, IUserInfoRepository userInfoRepository, IArtInfoRepository artInfoRepository, ITransactionHistoryRepository transactionHistoryRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userInfoRepository = userInfoRepository;
            _transactionHistoryRepository = transactionHistoryRepository;
            _artInfoRepository = artInfoRepository;
        }
        public async Task<bool> PurchaseWithBalance(int userId, int artId)
        {
            var e = await _purchaseRepository.GetPurchaseByUserIdAndArtId(userId, artId);
            if (e == null)
            {
                try
                {
                    var buyingUser = await _userInfoRepository.GetUserById(userId);
                    var artWorkToBeBought = await _artInfoRepository.GetArtById(artId);
                    if (buyingUser!.Balance < artWorkToBeBought!.Price)
                    {
                        throw new Exception("You don't have enough balance");
                    }
                    if (buyingUser.CreatorId == artWorkToBeBought.CreatorId)
                    {
                        throw new Exception("You can't buy your own artwork");
                    }

                    //hover over region to see code summary, you don't have to expand

                    #region Update Buyer's Balance and Create Purchase
                    buyingUser.Balance -= artWorkToBeBought.Price;

                    var purchaseToBeAdded = new Purchase()
                    {
                        UserId = userId,
                        ArtId = artId,
                        Price = artWorkToBeBought.Price,
                    };

                    await _purchaseRepository.CreateNewPurchase(purchaseToBeAdded);

                    await _userInfoRepository.UpdateUser(buyingUser);
                    #endregion
                    #region Update Creator Balance
                    var creatorId = artWorkToBeBought.CreatorId;
                    var userInfoBaseOnCreatorId = await _userInfoRepository.GetUserByCreatorId(creatorId);
                    userInfoBaseOnCreatorId!.Balance += artWorkToBeBought.Price;
                    await _userInfoRepository.UpdateUser(userInfoBaseOnCreatorId);
                    #endregion
                    #region Add transaction history for buyer
                    var transactionHistoryBuyer = new TransactionHistory()
                    {
                        UserId = userId,
                        Note = "You bought " + artWorkToBeBought.ArtName,
                        Amount = artWorkToBeBought.Price,
                        TransactionType = BusinessObject.TransactionType.Buy,
                        TransactionDate = DateTime.UtcNow,
                    };

                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistoryBuyer);
                    #endregion
                    #region Add transaction history for receiver
                    var transactionHistoryReceiver = new TransactionHistory()
                    {
                        UserId = artWorkToBeBought.CreatorId,
                        Note = buyingUser.NickName + " have bought " + artWorkToBeBought.ArtName,
                        Amount = artWorkToBeBought.Price,
                        TransactionType = BusinessObject.TransactionType.Sell,
                        TransactionDate = DateTime.UtcNow,
                    };

                    await _transactionHistoryRepository.CreateTransactionHistory(transactionHistoryReceiver);
                    #endregion
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Something critical happened. Sorry.");
                }
            }
            else
            {
                throw new Exception("You can't buy an artwork twice.");
            }
        }
        public async Task<bool> PurchaseWithExternalParty(int userId, int artId)
        {
            var e = await _purchaseRepository.GetPurchaseByUserIdAndArtId(userId, artId);
            if (e == null)
            {
                var artWorkToBeBought = await _artInfoRepository.GetArtById(artId);
                var buyingUser = await _userInfoRepository.GetUserById(userId);
                if (buyingUser!.CreatorId == artWorkToBeBought!.CreatorId)
                {
                    throw new Exception("You can't buy your own artwork");
                }
                //hover over region to see code summary, you don't have to expand
                #region Create new purchase

                var purchaseToBeAdded = new Purchase()
                {
                    UserId = userId,
                    ArtId = artId,
                    Price = artWorkToBeBought.Price,
                };

                await _purchaseRepository.CreateNewPurchase(purchaseToBeAdded);
                #endregion
                #region Update Creator Balance
                var creatorId = artWorkToBeBought.CreatorId;
                var userInfoBaseOnCreatorId = await _userInfoRepository.GetUserByCreatorId(creatorId);
                userInfoBaseOnCreatorId!.Balance += artWorkToBeBought.Price;
                await _userInfoRepository.UpdateUser(userInfoBaseOnCreatorId);
                #endregion
                #region Add transaction history for buyer
                var transactionHistoryBuyer = new TransactionHistory()
                {
                    UserId = userId,
                    Note = "You bought " + artWorkToBeBought.ArtName,
                    Amount = artWorkToBeBought.Price,
                    TransactionType = BusinessObject.TransactionType.Buy,
                    TransactionDate = DateTime.UtcNow,
                };

                await _transactionHistoryRepository.CreateTransactionHistory(transactionHistoryBuyer);
                #endregion
                #region Add transaction history for receiver
                var transactionHistoryReceiver = new TransactionHistory()
                {
                    UserId = artWorkToBeBought.CreatorId,
                    Note = buyingUser.NickName + " have bought " + artWorkToBeBought.ArtName,
                    Amount = artWorkToBeBought.Price,
                    TransactionType = BusinessObject.TransactionType.Sell,
                    TransactionDate = DateTime.UtcNow,
                };

                await _transactionHistoryRepository.CreateTransactionHistory(transactionHistoryReceiver);
                #endregion
                return true;

            }
            else
            {
                throw new Exception("You can't buy an artwork twice.");
            }
        }
    }
}
