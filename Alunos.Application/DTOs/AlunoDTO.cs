using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alunos.Application.DTOs;

public sealed class AlunoDTO
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Nome deve conter no maximo {1} caracteres")]
    public string? Nome { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(200, ErrorMessage = "Email deve conter no maximo {1} caracteres")]
    public string? Email { get; set; }

    [Required]
    [Column(TypeName = "int(3)")]
    public int Idade { get; set; }
}
