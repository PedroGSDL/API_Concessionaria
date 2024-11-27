using Microsoft.EntityFrameworkCore;
using MinimalApi.DTOs;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.Db;
using MinimalAPI.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<iAdministradorServico, AdministradorServico>();


builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});
var app = builder.Build();
app.MapPost("/login", ([FromBody]LoginDTO loginDTO, iAdministradorServico AdministradorServico) =>
{
    if (AdministradorServico.Login(loginDTO) != null)
        return Results.Ok("Login com sucesso!");
    else
        return Results.Unauthorized();
});

app.Run();
