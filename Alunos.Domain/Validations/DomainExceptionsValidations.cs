namespace Alunos.Domain.Validations;

internal sealed class DomainExceptionsValidations : Exception
{
    public DomainExceptionsValidations(string error) : base(error)
    { }

    internal static void When(bool hasError, string message)
    {
        if (hasError)
            throw new DomainExceptionsValidations(message);
    }
}
