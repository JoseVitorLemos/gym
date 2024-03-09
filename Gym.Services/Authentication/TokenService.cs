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

namespace Gym.Services.Authentication.TokenService;

public class TokenService : ITokenService
{
    public string GetToken(string clainId, string clainEmail, string clainRole)
    {
        var token = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(CustomConfiguration.GetJWTSettings.Secret);

        var subject = new ClaimsIdentity(new[]
        {
            new Claim(nameof(ClaimNames.Id), clainId),
            new Claim(nameof(ClaimNames.Email), clainEmail),
            new Claim(ClaimTypes.Role, clainRole)
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.AddHours(CustomConfiguration.GetJWTSettings.ExpireHours),
            SigningCredentials = credentials
        };

        return token.WriteToken(token.CreateToken(tokenDescriptor));
    }

    public LoginResponseDTO ResponseAuth(string id, string email, Roles role)
    {
        Validations(email, role);

        var response = new LoginResponseDTO
        {
            Token = GetToken(Convert.ToString(id), email, role.ToString())
        };

        return response;
    }

    private void Validations(string email, Roles role)
    {
        GlobalException.When(string.IsNullOrEmpty(email), "Email cannot be null");
        GlobalException.When(!EnumValidations.IsValidEnum<Roles>(role), "Invalid Role provided");
    }
}
