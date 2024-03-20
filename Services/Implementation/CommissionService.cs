using AzureBlobStorage;
using BusinessObject;
using BusinessObject.MongoDbObject;
using BusinessObject.SqlObject;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Utils.ImageUtil;

namespace Services.Implementation
{
    public class CommissionService : ICommissionService
    {
        private readonly IUserInfoRepository _userRepository;
        private readonly ICommissionRepository _commissionRepository;
        private readonly IImageUtil _imageUtil;
        private readonly IAzureBlobStorage _azureBlobStorage;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly ICommissionImageInfoRepository _commissionImageInfoRepository;
        public CommissionService(
            ICommissionRepository commissionRepository, 
            IUserInfoRepository userInfoRepository, 
            ITransactionHistoryRepository transactionHistoryRepository, 
            IImageUtil imageUtil,
            IAzureBlobStorage azureBlobStorage,
            ICommissionImageInfoRepository commissionImageInfoRepository)
        {
            _azureBlobStorage = azureBlobStorage;
            _commissionRepository = commissionRepository;
            _userRepository = userInfoRepository;
            _transactionHistoryRepository = transactionHistoryRepository;
            _imageUtil = imageUtil;
            _commissionImageInfoRepository = commissionImageInfoRepository;
        }

        public async Task<(bool isUpdated, string message)> AcceptCommission(int creatorId, int commissionId)
        {
            Commission? commission = await _commissionRepository.GetCommission(commissionId);
            if (commission == null || commission.CreatorId != creatorId)
            {
                return (false, $"Can not find commission with id {commissionId}");
            }
            if (commission.CommissionStatus != CommissionStatus.Pending)
            {
                return (false, "You can only accept pending commission");
            }
            commission.CommissionStatus = CommissionStatus.Accepted;
            await _commissionRepository.UpdateCommission(commission);
            return (true, "The commission has been updated successfully");
        }
        public Task<(bool isUpdated, string message)> CancelCommission(int userId, int commissionId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isCreated, string message)> CreateCommission(DateTime deadline, double price, int creatorId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isUpdated, string message)> CreatorCancelCommission(int artistId, int commissionId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isUpdated, string message)> DenyCommission(int creatorId, int commissionId)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool isUpdated, string message)> FinishCommission(int creatorId, int commissionId, IFormFile image)
        {
            Commission? commission = await _commissionRepository.GetCommission(commissionId);
            if (commission == null || commission.CreatorId != creatorId)
            {
                return (false, $"Can not found commission with id {commissionId}");
            }
            if (commission.CommissionStatus != CommissionStatus.Accepted)
            {
                return (false, $"Can not finish unaccepted commission");
            }
            using MemoryStream stream = new();
            image.CopyTo( stream );
            byte[] originalImage = stream.ToArray();
            Task<string> uploadOriginal = _azureBlobStorage.UploadFileAsync(originalImage);
            Task<(string blobName, int blobSize)> uploadPreview = ProcessAndUploadPreviewImage(originalImage);
            await Task.WhenAll(uploadOriginal,uploadPreview);
            CommissionImageInfo imageInfo = new()
            {
                Preview = new UploadedFileInfo()
                {
                    BlobName = (await uploadPreview).blobName,
                    FileName = $"preview_{image.FileName}",
                    FileSize = (await uploadPreview).blobSize,
                    ImageType = image.ContentType
                },
                Original = new UploadedFileInfo()
                {
                    BlobName = await uploadOriginal,
                    FileName = image.FileName,
                    FileSize = originalImage.Length,
                    ImageType = image.ContentType
                }
            };
            commission.ImageId = imageInfo.ImageId;
            await Task.WhenAll(
                _commissionImageInfoRepository.CreateNewImageInfo(imageInfo),
                _commissionRepository.UpdateCommission(commission));
            return (true, "Commisssion has been updated");
        }
        public Task<List<Commission>> GetCreatorAcceptedCommissions(int artistId)
        {
            throw new NotImplementedException();
        }

        public Task<Commission?> GetCreatorCommission(int artistId, int commissionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Commission>> GetCreatorCommissions(int artistId)
        {
            throw new NotImplementedException();
        }

        public Task<Commission?> GetUserCommission(int userId, int commissionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Commission>> GetUserCommissions(int userId)
        {
            throw new NotImplementedException();
        }

        private async Task<(string blobName,int fileSize)> ProcessAndUploadPreviewImage(byte[] originalImage)
        {
            byte[] previewImage = _imageUtil
                .SetImage(originalImage)
                .Resize()
                .AddWatermark()
                .GetResult();
            return (await _azureBlobStorage.UploadFileAsync(previewImage), previewImage.Length);
        }
        /*public async Task<(bool isSuccess, string message)> CreateCommission(DateTime deadline, double price, int creatorId, int userId)
        {
            UserInfo? user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return (false, "Invalid user id");
            }
            if (user.Balance < price)
            {
                return (false, "You don't have enough money to create this commission");
            }
            Commission newCommission = new Commission
            {
                CreatedDate = DateTime.Today,
                Deadline = deadline,
                Price = price,
                CreatorId = creatorId,
                UserId = userId,
                CommissionStatus = CommissionStatus.Pending
            };
            await _commissionRepository.CreateNewCommission(newCommission);

            user.Balance -= price;

            await _userRepository.UpdateUser(user);
            TransactionHistory transactionHistory = new TransactionHistory
            {
                UserId = userId,
                Note = "",
                Amount = price,
                TransactionDate = DateTime.Today,
                TransactionType = TransactionType.RequestCommission
            };
            await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
            return (true, "Commission created successfully");
        }

        public async Task<(bool isSuccess, string message)> CreatorUpdateCommissionStatus(int commissionId, int creatorId, CommissionStatus status)
        {
            if (status == CommissionStatus.Accepted)
            {
                *//*VALIDATE HERE*//*
                commission.CommissionStatus = CommissionStatus.Accepted;
                await _commissionRepository.UpdateCommission(commission);
                return (true, "Commission has been accepted");
            }
            if (status == CommissionStatus.Denied)
            {
                commission.CommissionStatus = CommissionStatus.Denied;
                await _commissionRepository.UpdateCommission(commission);

                UserInfo user = await _userRepository.GetUserById(commission.UserId);
                user.Balance = user.Balance + commission.Price;
                await _userRepository.UpdateUser(user);
                TransactionHistory transactionHistory = new TransactionHistory
                {
                    UserId = commission.UserId,
                    Note = "",
                    Amount = commission.Price,
                    TransactionDate = DateTime.Today,
                    TransactionType = TransactionType.RequestCommissionDeny
                };
                await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
            }
            else if (status == "cancel")
            {
                *//* VALIDATE HERE*//*
                commission.CommissionStatus = CommissionStatus.Canceled;
                await _commissionRepository.UpdateCommission(commission);

                UserInfo user = await _userRepository.GetUserById(commission.UserId);
                user.Balance = user.Balance + commission.Price / 2;
                await _userRepository.UpdateUser(user);
                TransactionHistory transactionHistory = new TransactionHistory
                {
                    UserId = commission.UserId,
                    Note = "",
                    Amount = commission.Price / 2,
                    TransactionDate = DateTime.Today,
                    TransactionType = TransactionType.RequestCommissionCancel
                };
                await _transactionHistoryRepository.CreateTransactionHistory(transactionHistory);
            }
        }
*/
    }
}
