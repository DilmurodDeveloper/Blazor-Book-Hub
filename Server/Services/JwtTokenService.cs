using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazorBookHub.Server.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlazorBookHub.Server.Services
{
    public class JwtTokenService(IConfiguration config)
    {
        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? ""),
            new(ClaimTypes.Email, user.Email ?? "")
        };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["JwtSettings:Issuer"],
                audience: config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(config["JwtSettings:ExpiresInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
