using Microsoft.EntityFrameworkCore;
using MinimalApi.DTOs;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.Db;
using MinimalAPI.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Dominio.Entidades;
using Microsoft.VisualBasic;


#region Builder
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<iAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<iVeiculoServico, VeiculoServico>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

var app = builder.Build();

#endregion
#region Home
app.MapGet("/", () => Results.Content(
    @"<!DOCTYPE html>
    <html>
    <head>
    <title>API de Veículos - Minimal API</title>
    <meta charset=""utf-8"" />
    <style>
        body {
            background-color: black;
            color: white;
            font-family: Arial, sans-serif;
            text-align: center;
            padding: 20px;
        }
        a {
            color: #1E90FF; 
            text-decoration: none;
        }
        a:hover {
            text-decoration: underline;
        }
    </style>
    </head>
    <body>
        <h1>Bem-vindo à API de veículos - Minimal API</h1>
        <p>Documentação: <a href='/swagger'>Swagger</a></p>
    </body>
    </html>",
    "text/html"
)).WithTags("Home");

#endregion

#region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, iAdministradorServico AdministradorServico) =>
{
    if (AdministradorServico.Login(loginDTO) != null)
        return Results.Ok("Login com sucesso!");
    else
        return Results.Unauthorized();
}).WithTags("Administradores");
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, iVeiculoServico veiculoServico) =>
{
    var novoVeiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };

    veiculoServico.Incluir(novoVeiculo);

    return Results.Created($"/veiculos/{novoVeiculo.Id}", novoVeiculo);
}).WithTags("Veículos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, iVeiculoServico veiculoServico) =>
{
    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);
}).WithTags("Veículos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, iVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();
    return Results.Ok(veiculo);
}).WithTags("Veículos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, iVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();

    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculo);
    return Results.Ok(veiculo);

}).WithTags("Veículos");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, iVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscaPorId(id);
    if (veiculo == null) return Results.NotFound();

    veiculoServico.Apagar(veiculo);

    return Results.NoContent();

}).WithTags("Veículos");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
#endregion