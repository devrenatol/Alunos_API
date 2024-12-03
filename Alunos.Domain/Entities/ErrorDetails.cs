using System.Text.Json;

namespace Alunos.Domain.Entities;

public sealed class ErrorDetails
{
    public string? Message { get; set; }
    public int StatusCode { get; set; }
    public string? Trace { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
