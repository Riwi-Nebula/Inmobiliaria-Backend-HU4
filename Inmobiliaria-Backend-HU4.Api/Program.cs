using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Inmobiliaria_Backend_HU4.Application.Interfaces;
using Inmobiliaria_Backend_HU4.Application.Services;
using Inmobiliaria_Backend_HU4.Domain.Interfaces;
using Inmobiliaria_Backend_HU4.Infrastructure.Data;
using Inmobiliaria_Backend_HU4.Infrastructure.Estensions;
using Inmobiliaria_Backend_HU4.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ===================== Conexion a la DB =====================
//Se obtiene la cadena de conexion desde el appsetings.json
builder.Services.AddInfraestructure(builder.Configuration);


// ===================== Inyeccion de dependencias =====================
// Repositorios
//Ejemplo: builder.ServicesAddScoped<IUserRepository, UserRepository>();


//Servicios
//Ejemplo: builder.Services.AddScoped<IUserService, UserService>();


// ===================== Configuracion JWT =====================
//Configura el sistema de autenticación para validar tokens JWT en las solicitudes HTTP.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// ===================== Controladores y Swagger =====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inmobiliaria API",
        Version = "v1",
        Description = "API para la administracion y gestion de una empresa inmoviliaria"
    });
});


// ===================== Configuracion del Cors (por si fron lo usa) =====================
var corsPolicyName = "AllowSpecificOrigins";

builder.Services.AddCors( options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod(); // si el front envía cookies o auth headers
        });
});


// ===================== Construcion y pipeline =====================
var app = builder.Build();

// Testiar la conexion a la db para tener control de posibles errores
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.OpenConnection();
        Console.WriteLine("Conexion a la base de datos exitosa.");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error en la conexion: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Administracion Inmobiliaria API v1");
        c.RoutePrefix = string.Empty;
    });
}
//Ejecutar este comando para poder hacer pruebas locales si hay algun problema:
//export ASPNETCORE_ENVIRONMENT=Local dotnet run --project ProductCatalog.Api

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();