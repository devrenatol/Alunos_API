using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Alunos.Infra.IoC.Filters;

public sealed class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu uma excecao nao tratada : Status Code 500");
        context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitacao : Status Code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
