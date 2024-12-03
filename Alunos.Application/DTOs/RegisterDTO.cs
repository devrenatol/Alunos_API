using System.ComponentModel.DataAnnotations;

namespace Alunos.Application.DTOs;

public sealed class RegisterDTO
{
    [Required(ErrorMessage = "O email deve ser informado")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha deve ser informada")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Confirmar Senha")]
    [Compare("Password", ErrorMessage = "As senhas nao conferem")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "O nome deve ser informado")]
    public string? UserName { get; set; }
}
