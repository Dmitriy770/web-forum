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

    public string GenerateToken(User user)
    {
        var claims = new Claim[]{new("UserId", user.Id.ToString())};
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddHours(_jwtOptions.ExpiresHours)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}