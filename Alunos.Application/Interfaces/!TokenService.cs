using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Alunos.Application.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateTokenJwt(IEnumerable<Claim> claims, IConfiguration configuration);
}
