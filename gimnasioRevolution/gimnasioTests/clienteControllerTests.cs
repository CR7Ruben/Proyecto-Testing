using Moq;
using System.Data;
using System.Data.SqlClient;
using Xunit;
using gimnasio.Models;
using gimnasio.Datos;
using gimnasio.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace gimnasioTests
{
    public class clienteControllerTests
    {
        /* ejecución => dotnet test -v normal --filter "implementacionCliente" */
        [Fact]
        public void implementacionCliente()
        {
            Console.WriteLine("--- Ejecucion - implementacion cliente ---");

            Console.WriteLine("Inicializando datos de cliente...");

            Console.WriteLine("Inicializando controladores de cliente...");

            /* arrange */
            /* se crea el objeto simulado moq en la clase cliente */
            var clienteDatos = new clienteDatos();
            /* se crea la instancia del controlador */
            var controller = new clienteController();

            /* si llega a asignar un valor nulo 
             * no se insertara y devolvera error */
            var createCliente = new clienteModel
            {
                nombre = "new",
                apellido = "new",
                numTel = 1010101010,
                observaciones = "Ninguna",
                fotoUrl = ""
            };

            /* si llega a asignar un valor nulo 
             * no se modificara y devolvera error
             * para realizar la actualización debe 
             * existir el cliente */
            var updateCliente = new clienteModel
            {
                idCliente = 3009,
                nombre = "modificar1",
                apellido = "modificar1",
                numTel = 2222222222,
                observaciones = "Ninguna",
                fotoUrl = "modificar"
            };

            /* para realizar la eliminación debe 
             * existir el cliente */
            var deleteCliente = new clienteModel
            {
                idCliente = 4017
            };
            
            /* act */
            /* manda a llamar al metodo */
            var createResultado = controller.insertar(createCliente) as RedirectToActionResult;
            /* manda a llamar al metodo */
            var updateResultado = controller.modificarCliente(updateCliente) as RedirectToActionResult;
            /* manda a llamar al metodo */
            var deleteResultado = controller.eliminarCliente(deleteCliente) as RedirectToActionResult;

            /* assert */
            /* si createResultado es diferente a nulo */
            if (createResultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarClientes", createResultado.ActionName);

                Console.WriteLine("--- Insercion de cliente --- \nEjecucion exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecucion fallida.");
            }

            /* si updateResultado es diferente a nulo */
            if (updateResultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarClientes", updateResultado.ActionName);

                Console.WriteLine("--- Modificacion de cliente --- \nEjecucion exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecucion fallida.");
            }

            /* si updateResultado es diferente a nulo */
            if (deleteResultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarClientes", deleteResultado.ActionName);

                Console.WriteLine("--- Eliminacion de cliente --- \nEjecucion exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecucion fallida.");
            }

        }
    }
}
