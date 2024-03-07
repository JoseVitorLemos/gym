using System.Text;
using Gym.DependencyInversion;
using Gym.DependencyInversion.Swagger;
using Gym.Helpers.ConfigurationManager;
using Gym.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddInfraInjection();
services.AddInfrastructureSwagger();
services.AddControllers();

services.Configure<FormOptions>(opt =>
{
    opt.MultipartBodyLengthLimit = (10 * 1024 * 1024);
});

services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CustomConfiguration.GetAppSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

services.AddLogging();

services.AddTransient<GlobalExceptionHandling>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandling>();

app.MapControllers();

app.Run();
