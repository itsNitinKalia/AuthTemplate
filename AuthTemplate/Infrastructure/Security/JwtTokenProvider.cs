using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthTemplate.Application.Auth.Interfaces;
using AuthTemplate.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthTemplate.Infrastructure.Security;

public sealed class JwtTokenProvider(IOptions<JwtOptions> options) : ITokenProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()),
            new(ClaimTypes.Role, user.Role)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
