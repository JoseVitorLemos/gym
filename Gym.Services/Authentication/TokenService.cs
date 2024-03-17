using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gym.Domain.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;
using Gym.Services.Authentication.TokenService.Enum;
using Gym.Services.DTO;
using Microsoft.IdentityModel.Tokens;
using Gym.Helpers.ConfigurationManager;
using Gym.Domain.Entities;
using Gym.Business.Utils;

namespace Gym.Services.Authentication.TokenService;

public class TokenService : ITokenService
{
    private string GetToken(Login login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(CustomConfiguration.JWTSettings.Secret);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(ListClaims(login)),
            Expires = DateTime.UtcNow.AddHours(CustomConfiguration.JWTSettings.ExpireHours),
            SigningCredentials = credentials
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    private IEnumerable<Claim> ListClaims(Login login)
        => new[]
        {
            new Claim(nameof(ClaimNames.Id), login.Id.ToString()),
            new Claim(nameof(ClaimNames.Email), login.Email),
            new Claim(ClaimTypes.Role, login.Role.ToString())
        };

    public LoginResponseDTO ResponseAuth(Login login)
    {
        Validations(login.Email, login.Role);

        var response = new LoginResponseDTO
        {
            Token = GetToken(login)
        };

        return response;
    }

    public static ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CustomConfiguration.JWTSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invlid token provided");

        return principal;
    }

    private static string GenerateSecret()
        => RandomHelpers.GenerateRandom(64, specialCharacters: true);

    private void Validations(string email, Roles role)
    {
        GlobalException.When(string.IsNullOrEmpty(email), "Email cannot be null");
        GlobalException.When(!EnumValidations.IsValidEnum<Roles>(role), "Invalid Role provided");
    }
}
