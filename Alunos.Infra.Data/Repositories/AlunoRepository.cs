using Alunos.Domain.Entities;
using Alunos.Domain.Interfaces;
using Alunos.Infra.Data.Context;

namespace Alunos.Infra.Data.Repositories;

public sealed class AlunoRepository : Repository<Aluno>, IAlunoRepository
{
    public AlunoRepository(AppDbContext context) : base(context)
    { }
}
