using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebForum.Auth.Application.Interfaces;
using WebForum.Auth.Domain.Models;
using WebForum.Auth.Infrastructure.Options;

namespace WebForum.Auth.Infrastructure.Utilities;

public sealed class JwtProvider(
    IOptions<JwtOptions> jwtOptions
) : IJwtProvider
{
    private readonly SigningCredentials _signingCredentials = new (
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),
        SecurityAlgorithms.HmacSha256
    );
    
    public (string token, AuthInfo authInfo) GenerateToken(User user)
    {
        var claims = new Claim[]
        {
            new("UserId", user.Id.ToString()),
            new(nameof(user.Permissions), user.Permissions.ToString())
        };

        var expires = DateTime.Now.AddHours(jwtOptions.Value.ExpiresHours);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: _signingCredentials,
            expires: expires
        );
        var strToken = new JwtSecurityTokenHandler().WriteToken(token);
        var authInfo = AuthInfo.Create(
            user.Id,
            user.Login,
            user.Permissions,
            expires
        );

        return (strToken, authInfo);
    }

    public async Task<(bool isValid, Guid userId)> ValidateToken(string accessToken)
    {
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingCredentials.Key,
                
                ValidateAudience = false,
                ValidateIssuer = false
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(accessToken, validationParameters);
            
            var userId = Guid.Empty;
            if (tokenValidationResult.IsValid)
            {
                tokenValidationResult.Claims.TryGetValue("UserId", out var userIdObject);
                userId = Guid.Parse(userIdObject.ToString());
            }
            
            return (tokenValidationResult.IsValid, userId);
    }
}