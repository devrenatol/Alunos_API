using Alunos.Application.DTOs;
using Alunos.Domain.Entities;
using AutoMapper;

namespace Alunos.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Aluno, AlunoDTO>().ReverseMap();
    }
}
