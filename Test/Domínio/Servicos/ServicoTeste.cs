using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.Db;

namespace Test.Dominio.Entidades
{
    [TestClass]
    public class ServicoTeste
    {
        // Método para criar o contexto de teste
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", ".."));
            var builder = new ConfigurationBuilder()
                .SetBasePath(path ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();  // Corrigido o nome do método

            var configuration = builder.Build();
            return new DbContexto(configuration);
        }

        [TestMethod]
        public void TestarSalvarAdministrador()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores");
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            var administradorServico = new AdministradorServico(context);

            // Act
            administradorServico.Incluir(adm);

            // Assert
            Assert.AreEqual(1, administradorServico.Todos(1).Count());
        }


        [TestMethod]
        public void TestarBuscaAdmPorId()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores");

            var adm = new Administrador
            {
                Email = "teste@teste.com",
                Senha = "teste",
                Perfil = "Adm"
            };

            var administradorServico = new AdministradorServico(context);

            // Act
            administradorServico.Incluir(adm);
            var administradorSalvo = administradorServico.BuscaPorId(adm.Id);

            // Assert
            Assert.IsNotNull(administradorSalvo); // Verifica se encontrou o administrador
            Assert.AreEqual(adm.Id, administradorSalvo.Id); // Verifica se os IDs são iguais
        }

        [TestMethod]
        public void TestarSalvarVeiculo()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");
            var car = new Veiculo();
            car.Marca = "teste";
            car.Nome = "teste";
            car.Ano = 2000;
            var veiculoServico = new VeiculoServico(context);

            // Act
            veiculoServico.Incluir(car);

            // Assert
            Assert.AreEqual(1, veiculoServico.Todos(1).Count());
        }

        [TestMethod]
        public void TestarBuscaVeiculoPorId()
        {
            // Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");
            var car = new Veiculo();
            car.Marca = "teste";
            car.Nome = "teste";
            car.Ano = 2000;
            var veiculoServico = new VeiculoServico(context);

            // Act
            veiculoServico.Incluir(car);
            var carsalvo = veiculoServico.BuscaPorId(car.Id);

            // Assert
            Assert.IsNotNull(carsalvo); // Verifica se encontrou o administrador
            Assert.AreEqual(car.Id, carsalvo.Id); // Verifica se os IDs são iguais
        }
    }
}

