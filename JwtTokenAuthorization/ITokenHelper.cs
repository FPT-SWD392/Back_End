using BusinessObject.SqlObject;
using Microsoft.AspNetCore.Http;
namespace JwtTokenAuthorization
{
    public interface ITokenHelper
    {
        public string GenerateToken(User user);
        public string GetUserIdFromToken(HttpContext httpContext);
    }
}
