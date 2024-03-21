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
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public (string token, DateTime expiresIn) GenerateToken(User user)
    {
        var claims = new Claim[]
        {
            new("UserId", user.Id.ToString()),
            new(nameof(user.Permissions), user.Permissions.ToString())
        };
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var expires = DateTime.Now.AddHours(_jwtOptions.ExpiresHours);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expires
        );
        var strToken = new JwtSecurityTokenHandler().WriteToken(token);

        return (strToken, expires);
    }
}