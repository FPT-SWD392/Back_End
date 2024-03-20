using BusinessObject.MongoDbObject;
using BusinessObject.SqlObject;
using Microsoft.AspNetCore.Http;
namespace JwtTokenAuthorization
{
    public interface ITokenHelper
    {
        public string GenerateToken(UserInfo user);
        public string? GetUserIdFromToken(HttpContext httpContext);
        public string? GetCreatorIdFromToken(HttpContext httpContext);
        public string? GetAdminIdFromToken(HttpContext httpContext);
        public string GenerateSysAdminToken();
        public string GenerateAdminToken(AdminAccount adminAccount);
    }
}
