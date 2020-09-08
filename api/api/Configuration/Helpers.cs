using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api.Configuration
{
    public static class Helpers
    {
        internal static (string, DateTime) GenerateToken(string id, string emailAddress)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, emailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", id)
            };

            const int durationInDays = 100 * 365; // 100 years
            var expiry = DateTime.UtcNow.AddDays(durationInDays);
            var token = new JwtSecurityToken
            (
                Config.Issuer,
                Config.Audience,
                claims,
                expires: expiry,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.Secret)),
                    SecurityAlgorithms.HmacSha256)
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
        }

        internal static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (!(claimsPrincipal.Identity is ClaimsIdentity identity))
            {
                throw new Exception("User identity could not be retrieved.");
            }

            return identity.Claims.First(x => x.Type == "id").Value;
        }
    }
}