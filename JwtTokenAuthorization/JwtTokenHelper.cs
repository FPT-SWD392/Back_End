using BusinessObject.SqlObject;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenAuthorization
{
    public class JwtTokenHelper : ITokenHelper
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;
        public JwtTokenHelper(IConfiguration configuration)
        {
            _issuer = configuration["Jwt:Issuer"] 
                ?? throw new Exception("Can not find jwt issuer in config file");
            _audience = configuration["Jwt:Audience"] 
                ?? throw new Exception("Can not find jwt audience in config file");
            _key = configuration["Jwt:SecretKey"] 
                ?? throw new Exception("Can not find jwt secret key in config file");
        }
        public string GenerateToken(User user)
        {
            SymmetricSecurityKey symmetricKey = new(Encoding.UTF8.GetBytes(_key));
            SigningCredentials credential = new(symmetricKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new()
            {
                new(CustomClaimType.Role,user.Role.ToString()),
                new(CustomClaimType.UserId,user.UserId)
            };
            JwtSecurityToken token = new(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GetUserIdFromToken(HttpContext httpContext)
        {
            if (false == httpContext.Request.Headers.ContainsKey("Authorization"))
                throw new Exception("Need authorization");
            string? authorizationString = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(authorizationString) || false == authorizationString.StartsWith("Bearer "))
                throw new Exception("Invalid token");
            string jwtTokenString = authorizationString["Bearer ".Length..];
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(jwtTokenString);

            Claim? idClaim = jwtToken.Claims.SingleOrDefault(claim => claim.Type == CustomClaimType.UserId);
            return idClaim?.Value ?? throw new Exception("Invalid token");
        }
    }
    public static class CustomClaimType
    {
        public static string UserId { get; } = "UserId";
        public static string Role { get; } = "UserRole";
    }
}
