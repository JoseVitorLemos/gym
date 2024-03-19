using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gym.Business.Utils;
using Gym.Domain.Enums;
using Gym.Helpers.ConfigurationManager;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;
using Gym.Infrastructure.Caching;
using Gym.Services.Authentication.TokenService.Enum;
using Gym.Services.DTO;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Gym.Services.Authentication.TokenService;

public class TokenService : ITokenService
{
    private readonly ICacheService _cache;

    public TokenService(ICacheService cache)
    {
        _cache = cache;
        _cache.SetDistributedCacheTimes(TimeSpan.FromDays(30),
                TimeSpan.FromDays(30));
    }

    private string CreateToken(LoginDTO login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(CustomConfiguration.JWTSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(ListClaims(login)),
            Expires = DateTime.UtcNow.AddHours(CustomConfiguration.JWTSettings.ExpireHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


    private static string CreateRefreshToken()
        => RandomHelpers.GenerateRandom(64);

    private IEnumerable<Claim> ListClaims(LoginDTO login)
        => new[]
        {
            new Claim(nameof(ClaimNames.Id), login.Id.ToString()),
            new Claim(nameof(ClaimNames.Email), login.Email),
            new Claim(ClaimTypes.Role, login.Role.ToString())
        };

    public async Task<LoginResponseDTO> ResponseAuth(LoginDTO login)
    {
        Validations(login.Email, login.Role);

        var response = new LoginResponseDTO
        {
            Token = CreateToken(login),
            RefreshToken = CreateRefreshToken()
        };

        await SaveRefreshToken(login.Email,
                response.RefreshToken);

        return response;
    }

    public async Task<LoginResponseDTO> ResponseAuth(LoginDTO login,
            string refreshToken)
    {
        var cacheRefreshToken = await GetToken(login.Email);

        if (string.IsNullOrEmpty(cacheRefreshToken))
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Refresh token are expired");

        if (cacheRefreshToken != refreshToken)
            throw new GlobalException(HttpStatusCodes.BadRequest,
                            "Refresh token is invalid");

        var response = new LoginResponseDTO
        {
            Token = CreateToken(login),
            RefreshToken = cacheRefreshToken
        };

        return response;
    }

    private async Task<string> GetToken(string email)
        => await _cache.GetAsync(email);

    private async Task SaveRefreshToken(string email, string refreshToken)
        => await _cache.SetAsync(email, refreshToken);

    private async Task RevokeRefreshToken(string email)
        => await _cache.DeleteAsync(email);

    private static TokenValidationParameters GetParameters()
        => new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(CustomConfiguration.JWTSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

    private void Validations(string email, Roles role)
    {
        GlobalException.When(string.IsNullOrEmpty(email), "Email cannot be null");
        GlobalException.When(!EnumValidations.IsValidEnum<Roles>(role), "Invalid Role provided");
    }
}
