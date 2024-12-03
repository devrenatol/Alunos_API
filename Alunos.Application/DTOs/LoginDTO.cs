using System.ComponentModel.DataAnnotations;

namespace Alunos.Application.DTOs;

public sealed class LoginDTO
{
    [Required(ErrorMessage = "O email deve ser informado")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha deve ser informada")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
