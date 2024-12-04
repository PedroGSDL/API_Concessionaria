using MinimalAPI.Dominio.Entidades;

namespace Test.Domínio.Entidades;
[TestClass]
public class AdministradorTeste
{
    [TestMethod]
    public void TestarGetSetPropriedadesVeiculo()
    {
        //Arrange
        var car = new Veiculo();

        //Act
        car.Id = 1;
        car.Marca = "teste";
        car.Nome = "teste";
        car.Ano = 2001;

        //Assert
        Assert.AreEqual(1, car.Id);
        Assert.AreEqual("teste", car.Marca);
        Assert.AreEqual("teste", car.Nome);
        Assert.AreEqual(2001, car.Ano);
    }
}
