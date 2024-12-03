using Alunos.Application.DTOs;
using Alunos.Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlunosAPI.Controllers;

[Route("api/v{version:ApiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AlunosController : ControllerBase
{
    private readonly IAlunoServices _services;
    private readonly ILogger<AlunosController> _logger;

    public AlunosController(IAlunoServices services, ILogger<AlunosController> logger)
    {
        _services = services;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetTodosAlunos()
    {
        var alunos = await _services.GetAllAsync();

        if (alunos is null)
        {
            _logger.LogError("Nenhum aluno cadastrado");
            return NotFound("Nenhum aluno cadastrado");
        }

        return Ok(alunos);
    }

    [HttpGet("{id:int}", Name = "ObterAluno")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AlunoDTO>> GetAluno(int id)
    {
        var aluno = await _services.GetAsync(id);

        if (aluno is null)
        {
            _logger.LogError("Aluno nao encontrado");
            return NotFound("Aluno nao encontrado");
        }

        return Ok(aluno);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AlunoDTO>> PostAluno(AlunoDTO alunoDTO)
    {
        if (alunoDTO is null)
        {
            _logger.LogError("Erro com informacoes sobre o aluno");
            return BadRequest("Erro com informacoes sobre o aluno");
        }

        var alunoAdicionadoDTO = await _services.PostAsync(alunoDTO);

        if (alunoAdicionadoDTO is null)
        {
            _logger.LogError("Erro com informacoes sobre o aluno");
            return BadRequest("Erro com informacoes sobre o aluno");
        }

        return new CreatedAtRouteResult("ObterAluno", new { id = alunoAdicionadoDTO.Id }, alunoAdicionadoDTO);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<AlunoDTO>> PutAluno(int id, AlunoDTO alunoDTO)
    {
        if (alunoDTO.Id != id || alunoDTO is null)
        {
            _logger.LogError("Erro com informacoes sobre o aluno");
            return BadRequest("Erro com informacoes sobre o aluno");
        }

        var alunoModificadoDTO = await _services.PutAsync(id, alunoDTO);

        if (alunoModificadoDTO is null)
        {
            _logger.LogError("Erro com informacoes sobre o aluno");
            return BadRequest("Erro com informacoes sobre o aluno");
        }

        return Ok(alunoModificadoDTO);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteAluno(int id)
    {
        var alunoDTO = await _services.GetAsync(id);

        if (alunoDTO is null)
        {
            _logger.LogError("Aluno nao encontrado");
            return NotFound("Aluno nao encontrado");
        }

        await _services.DeleteAsync(id);

        return NoContent();
    }
}
