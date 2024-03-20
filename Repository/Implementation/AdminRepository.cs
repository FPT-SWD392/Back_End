using BusinessObject;
using BusinessObject.MongoDbObject;
using DataAccessObject;
using Repository.Interface;

namespace Repository.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IGenericDao<AdminAccount> _adminAccountDao;
        public AdminRepository(IDaoFactory daoFactory)
        {
            _adminAccountDao = daoFactory.CreateDao<AdminAccount>();
        }
        public async Task<AdminAccount?> GetAdminAccount(int id)
        {
            return await _adminAccountDao
                .Query()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
        public async Task<AdminAccount?> GetAdminAccount(string email)
        {
            return await _adminAccountDao
                .Query()
                .Where(x => x.Email == email)
                .SingleOrDefaultAsync();
        }
        public async Task<bool> CreateNewAdmin(AdminAccount adminAccount)
        {
            long existedEmailCount = await _adminAccountDao
                .Query()
                .Where(x=>x.Email == adminAccount.Email)
                .CountAsync();
            if (existedEmailCount > 0)
            {
                return false;
            }
            await _adminAccountDao.CreateAsync(adminAccount);
            return true;
        }
        public async Task<bool> DisableAdmin(int adminId)
        {
            AdminAccount? admin = await _adminAccountDao
                .Query()
                .Where(x=>x.Id == adminId && x.Status == AdminAccountStatus.Enable)
                .SingleOrDefaultAsync();
            if (admin == null)
            {
                return false;
            }
            admin.Status = AdminAccountStatus.Disable;
            await _adminAccountDao.UpdateAsync(admin);
            return true;
        }
        public async Task<bool> EnableAdmin(int adminId)
        {
            AdminAccount? admin = await _adminAccountDao
                .Query()
                .Where(x => x.Id == adminId && x.Status == AdminAccountStatus.Disable)
                .SingleOrDefaultAsync();
            if (admin == null)
            {
                return false;
            }
            admin.Status = AdminAccountStatus.Enable;
            await _adminAccountDao.UpdateAsync(admin);
            return true;
        }
    }
}
