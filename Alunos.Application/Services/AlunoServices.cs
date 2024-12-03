using Alunos.Application.DTOs;
using Alunos.Application.Interfaces;
using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using AutoMapper;

namespace Alunos.Application.Services;

public class AlunoServices : IAlunoServices
{
    private readonly IAlunoRepository _repository;
    private readonly IMapper _mapper;

    public AlunoServices(IAlunoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AlunoDTO>> GetAllAsync()
    {
        var alunos = await _repository.GetAllAsync();

        var alunosDTO = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);

        return alunosDTO;
    }

    public async Task<AlunoDTO> GetAsync(int id)
    {
        var aluno = await _repository.GetAsync(id);

        if (aluno is null)
            throw new ArgumentNullException(nameof(aluno));

        var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

        return alunoDTO;
    }

    public async Task<AlunoDTO> PostAsync(AlunoDTO alunoDTO)
    {
        if (alunoDTO is null)
            throw new ArgumentNullException(nameof(alunoDTO));

        var aluno = _mapper.Map<Aluno>(alunoDTO);
        var alunoAdicionado = await _repository.PostAsync(aluno);
        var alunoAdicionadoDTO = _mapper.Map<AlunoDTO>(alunoAdicionado);

        return alunoAdicionadoDTO;
    }

    public async Task<AlunoDTO> PutAsync(int id, AlunoDTO alunoDTO)
    {
        if (alunoDTO.Id != id)
            throw new InvalidOperationException(nameof(alunoDTO));

        var aluno = _mapper.Map<Aluno>(alunoDTO);
        var alunoModificado = await _repository.PutAsync(aluno);
        var alunoModificadoDTO = _mapper.Map<AlunoDTO>(alunoModificado);

        return alunoModificadoDTO;
    }

    public async Task DeleteAsync(int id)
    {
        var aluno = await _repository.GetAsync(id);

        if (aluno is null)
            throw new ArgumentNullException(nameof(aluno));

        await _repository.DeleteAsync(aluno);
    }
}
