using Alunos.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alunos.Application.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GenerateTokenJwt(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var key = configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
            throw new InvalidOperationException("Chave secreta invalida");

        var privateKey = Encoding.UTF8.GetBytes(key);

        var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
            Audience = configuration.GetSection("JWT").GetValue<string>("ValidAudience"),
            Issuer = configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
            SigningCredentials = signinCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return token;
    }
}
