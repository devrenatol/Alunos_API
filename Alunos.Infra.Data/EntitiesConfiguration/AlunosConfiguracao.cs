using Alunos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alunos.Infra.Data.EntitiesConfiguration;

internal sealed class AlunosConfiguracao : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome).HasMaxLength(150).IsRequired();
        builder.Property(a => a.Email).HasMaxLength(200).IsRequired();
        builder.Property(a => a.Idade).HasPrecision(3);

        builder.HasData(
            new Aluno(1, "Aluno 1", "aluno1@email.com", 30),
            new Aluno(2, "Aluno 2", "aluno2@email.com", 40)
            );
    }
}
