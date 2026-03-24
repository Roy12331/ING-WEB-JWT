using DbModel.demoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mvc.Api.extensions;
using Mvc.Api.SeedData;
using Mvc.Bussnies.Auth;
using Mvc.Bussnies.Persona;
using Mvc.Bussnies.PersonaTipoB;
using Mvc.Bussnies.PersonaTipoDocumentoB;
using Mvc.Bussnies.PersonaTipoSangreB;
using Mvc.Bussnies.PersonaTipoSexoB;
using Mvc.Repository.PersonaRepos.PersonaRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaRepo.Implementacion;
using Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Implementacion;
using Mvc.Repository.PersonaRepos.PersonaTipoRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoRepo.Implementacion;
using Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Implementacion;
using Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Implementacion;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_demoContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inyección de dependencias - Repositories
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaBussnies, PersonaBussnies>();

builder.Services.AddScoped<IPersonaTipoDocumentoRepository, PersonaTipoDocumentoRepository>();
builder.Services.AddScoped<IPersonaTipoDocumentoBussnies, PersonaTipoDocumentoBussnies>();

builder.Services.AddScoped<IPersonaTipoRepository, PersonaTipoRepository>();
builder.Services.AddScoped<IPersonaTipoBussnies, PersonaTipoBussnies>();

builder.Services.AddScoped<IPersonaTipoSangreRepository, PersonaTipoSangreRepository>();
builder.Services.AddScoped<IPersonaTipoSangreBussnies, PersonaTipoSangreBussnies>();

builder.Services.AddScoped<IPersonaTipoSexoRepository, PersonaTipoSexoRepository>();
builder.Services.AddScoped<IPersonaTipoSexoBussnies, PersonaTipoSexoBussnies>();

builder.Services.AddScoped<IAuthBussnies, AuthBussnies>();

// Configurar JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero // Eliminar el tiempo de gracia predeterminado de 5 minutos
    };
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware(typeof(ErrorMiddleware));
app.UseCors("AllowAll");

app.UseAuthentication(); // Agregar autenticación antes de autorización
app.UseAuthorization();

app.MapControllers();

await DemoDataSeeder.SeedAsync(app.Services);

app.Run();
