using Alunos.Application.DTOs;
using Alunos.Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AlunosAPI.Controllers;

[Route("api/v{version:ApiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ITokenService tokenService, UserManager<IdentityUser> userManager,
        IConfiguration config, ILogger<AccountController> logger)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _config = config;
        _logger = logger;
    }

    [HttpPost("LoginUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenService.GenerateTokenJwt(authClaims, _config);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Validade = token.ValidTo
            });
        }

        return Unauthorized();
    }

    [HttpPost("CreateUser")]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            _logger.LogError("Senhas diferentes");
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { StatusCode = "Erro", Message = "Senhas diferentes" });
        }

        var userExists = await _userManager.FindByEmailAsync(model.Email!);

        if (userExists is not null)
        {
            _logger.LogError("Usuario ja existe");
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { StatusCode = "Erro", Message = "Usuario ja existe" });
        }

        var user = new IdentityUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName,
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            _logger.LogError("Criacao do Usuario falhou");
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { StatusCode = "Erro", Message = "Criacao do Usuario falhou" });
        }

        return Ok(new ResponseDTO() { StatusCode = "Sucesso", Message = "Usuario criado com sucesso!" });
    }
}
