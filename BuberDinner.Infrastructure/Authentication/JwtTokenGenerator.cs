using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator(IDateTimeProvider _dateTimeProvider) : IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("super-secret-key-that-is-32-bytes")),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: "BuberDinner",
            audience: "BuberDinner.Api",
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}