using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Alunos.Infra.IoC.AppExtensions;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    public static void ConfigureCors(this IApplicationBuilder app)
    {
        app.UseCors("AplicacaoReactPermitida");
    }
}
