using Alunos.Infra.IoC.AppExtensions;
using Alunos.Infra.IoC.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDependencyInjections();
builder.AddInfraSwagger();
builder.AddTokenJwtConfig();


var app = builder.Build();
var environment = app.Environment;

app.UseSwaggerMiddleware(environment);
app.ConfigureExceptionsHandler(environment);

app.UseHttpsRedirection();
app.UseRouting();

app.ConfigureCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
