using Alunos.Application.Interfaces;
using Alunos.Application.Mapping;
using Alunos.Application.Services;
using Alunos.Domain.Interfaces;
using Alunos.Infra.Data.Context;
using Alunos.Infra.Data.Repositories;
using Alunos.Infra.IoC.Filters;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alunos.Infra.IoC.ServicesExtensions;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddDependencyInjections(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiExceptionFilter));
        });

        builder.Services.AddApiVersioning(o =>
        {
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ReportApiVersions = true;
            o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AplicacaoReactPermitida", policy =>
            {
                policy.WithOrigins(builder.Configuration["CORS:FrontendUrl"])
                .AllowAnyMethod()
                .AllowAnyHeader();

            });
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<IAlunoServices, AlunoServices>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddSingleton<ITokenService>(new TokenService());

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        return builder;
    }
}
