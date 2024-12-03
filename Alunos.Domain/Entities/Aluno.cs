using Alunos.Domain.Validations;

namespace Alunos.Domain.Entities;

public sealed class Aluno
{
    public int Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public int Idade { get; private set; }

    public Aluno(int id, string nome, string email, int idade)
    {
        Validations(nome, email, idade);
        DomainExceptionsValidations.When(id < 0, "Id invalido");

        Id = id;
    }

    public Aluno(string nome, string email, int idade)
    {
        Validations(nome, email, idade);
    }

    private void Validations(string nome, string email, int idade)
    {
        DomainExceptionsValidations.When(nome.Length < 3, "Tamanho do nome invalido");
        DomainExceptionsValidations.When(string.IsNullOrWhiteSpace(nome), "Nome precisa ser preenchido");

        DomainExceptionsValidations.When(email.Length < 5, "Tamano do email invalido");
        DomainExceptionsValidations.When(string.IsNullOrWhiteSpace(email), "Email precisa ser preenchido");

        DomainExceptionsValidations.When(idade < 0, "Idade invalida");
        DomainExceptionsValidations.When(idade == 0, "Idade invalida");

        Nome = nome;
        Email = email;
        Idade = idade;
    }
}
