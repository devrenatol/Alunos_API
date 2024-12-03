using Alunos.Application.DTOs;
using Alunos.Domain.Entities;

namespace Alunos.Application.Interfaces;

public interface IAlunoServices
{
    Task<IEnumerable<AlunoDTO>> GetAllAsync();
    Task<AlunoDTO> GetAsync(int id);
    Task<AlunoDTO> PostAsync(AlunoDTO alunoDTO);
    Task<AlunoDTO> PutAsync(int id, AlunoDTO alunoDTO);
    Task DeleteAsync(int id);
}
