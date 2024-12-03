using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Alunos.Infra.IoC.ServicesExtensions;

public static class DependencyInjectionSwagger
{
    public static WebApplicationBuilder AddInfraSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
