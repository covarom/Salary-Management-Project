using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalaryManagement.Application.Common.Interfaces.Authentication;
using SalaryManagement.Application.Common.Interfaces.Services;
using SalaryManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalaryManagement.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }


        public string GenerateToken(Admin admin)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.AdminId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (!string.IsNullOrEmpty(admin.Name))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Name, admin.Name));
            }

            if (!string.IsNullOrEmpty(admin.Image))
            {
                claims.Add(new Claim("image", admin.Image));
            }

            if (!string.IsNullOrEmpty(admin.PhoneNumber))
            {
                claims.Add(new Claim("phoneNumber", admin.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(admin.Email))
            {
                claims.Add(new Claim("email", admin.Email));
            }

            if (!string.IsNullOrEmpty(admin.Username))
            {
                claims.Add(new Claim("username", admin.Username));
            }

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiredMinute),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
