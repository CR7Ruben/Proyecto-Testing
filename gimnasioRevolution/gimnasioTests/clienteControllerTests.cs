using Moq;
using System.Data;
using System.Data.SqlClient;
using Xunit;
using gimnasio.Models;
using gimnasio.Datos;

namespace gimnasioTests
{
    public class clienteControllerTests
    {

        [Fact]
        public void insertarCliente()
        {
            // Arrange
            var mockConnection = new Mock<SqlConnection>();
            var mockCommand = new Mock<SqlCommand>();
            var clienteDatos = new clienteDatos();

            var oCliente = new clienteModel
            {
                nombre = "Juan",
                apellido = "Da",
                numTel = 644,
                observaciones = "Ninguna",
                fotoUrl = "foto.jpg"
            };

            // Simula el comportamiento de la conexión y el comando
            mockConnection.Setup(m => m.Open());
            mockCommand.Setup(m => m.ExecuteNonQuery()).Returns(1); // Simula que la inserción fue exitosa

            // Act
            var resultado = clienteDatos.insertar(oCliente);

            // Assert
            Assert.True(resultado);
        }
    }
}
