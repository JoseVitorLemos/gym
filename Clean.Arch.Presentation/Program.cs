using System.Text;
using Clean.Arch.DependencyInversion;
using Clean.Arch.Helpers.Utils;
using Clean.Arch.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddSwaggerGen();
services.AddInfraInjection();
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(InfraHelpers.GetAuthSecret())),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

services.AddLogging();

services.AddTransient<GlobalExceptionHandling>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandling>();

app.MapControllers();

app.Run();
