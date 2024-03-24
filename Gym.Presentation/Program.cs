using System.Text.Json.Serialization;
using Gym.DependencyInversion;
using Gym.DependencyInversion.Caching;
using Gym.DependencyInversion.Swagger;
using Gym.Presentation.Middlewares;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddInfraInjection();
services.AddInfrastructureJWT();
services.AddInfrastructureSwagger();
services.AddInfrastructureRedis();
services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
services.AddLogging();

services.AddHttpContextAccessor();

services.Configure<FormOptions>(opt =>
{
    opt.MultipartBodyLengthLimit = (10 * 1024 * 1024);
});

services.AddTransient<GlobalExceptionHandling>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", "Gym API v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandling>();

app.MapControllers();

app.Run();
