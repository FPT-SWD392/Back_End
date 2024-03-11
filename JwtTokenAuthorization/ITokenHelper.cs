using BusinessObject.SqlObject;
using Microsoft.AspNetCore.Http;
namespace JwtTokenAuthorization
{
    public interface ITokenHelper
    {
        public string GenerateToken(UserInfo user);
        public string GetUserIdFromToken(HttpContext httpContext);
        public string GetCreatorIdFromToken(HttpContext httpContext);
    }
}
