using System.IdentityModel.Tokens.Jwt;

namespace SalaryManagement.Api.Common.Helper
{
    public class JwtTokenHelper
    {
        public static string GetClaimValue(string jwtToken, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var claim = token.Claims.FirstOrDefault(c => c.Type == claimType);

            return claim?.Value;
        }
    }
}
