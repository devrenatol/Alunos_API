using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alunos.Application.DTOs;

public sealed class ResponseDTO
{
    public string? StatusCode { get; set; }
    public string? Message { get; set; }
}
