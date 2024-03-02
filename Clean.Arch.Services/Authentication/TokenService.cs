using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clean.Arch.Helpers.Utils;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Arch.Services.Authentication.TokenService;

public class TokenService : ITokenService
{
    public string GetToken(string clainId, string clainEmail, string clainRole)
    {
        var token = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(InfraHelpers.GetAuthSecret());

        var subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, clainId),
            new Claim(ClaimTypes.Email, clainEmail),
            new Claim(ClaimTypes.Role, clainRole),
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.AddHours(InfraHelpers.GetExpireTime()),
            SigningCredentials = credentials
        };

        return token.WriteToken(token.CreateToken(tokenDescriptor));
    }
}
