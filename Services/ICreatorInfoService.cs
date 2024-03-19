using BusinessObject.DTO;

namespace Services
{
    public interface ICreatorInfoService
    {
        Task UpgradeToCreatorWithBalance(int userId, CreatorInfoDTO creatorInfoDTO);
        Task UpgradeToCreatorWithExternalParty(int userId, CreatorInfoDTO creatorInfoDTO);
    }
}
