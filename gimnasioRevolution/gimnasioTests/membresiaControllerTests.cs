using gimnasio.Controllers;
using gimnasio.Datos;
using gimnasio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gimnasioTests
{
    public class membresiaControllerTests
    {
        /* ejecucion => dotnet test -v normal --filter "implementacionMembresia" */
        [Fact]
        public void modificarMembresia()
        {
            Console.WriteLine("--- Ejecucion - modificacion de membresia --- \n");

            Console.WriteLine("Inicializando datos de membresía...\n");

            Console.WriteLine("Inicializando controladores de membresia...\n");

            /* arrange */
            /* se crea el objeto simulado moq en la clase membresia */
            var membresiaDatos = new membresiaDatos();
            /* se crea la instancia del controlador */
            var controller = new membresiaController();

            /* si llega a asignar un valor nulo 
             * no se insertara y devolvera error */

            var insertMembresia = new membresiaModel
            {
                membresia = "as",
                duracion = 10,
                precio = 10
            };

            /* si llega a asignar un valor nulo 
             * no se modificara y devolvera error
             * para realizar la actualización debe 
             * existir la membresia */
            var updateMembresia = new membresiaModel
            {
                idMembresia = 1,
                membresia = "as",
                duracion = 10,
                precio = 10
            };

            var deleteMmebresia = new membresiaModel
            {
                idMembresia = 1
            };

            /* act */
            /* manda a llamar al metodo */
            var insertResultado = controller.agregar(insertMembresia) as RedirectToActionResult;
            var updateResultado = controller.modificar(updateMembresia) as RedirectToActionResult;
            var deleteResultado = controller.eliminar(deleteMmebresia) as RedirectToActionResult;

            /* assert */
            /* si resultado es diferente a nulo */
            if (insertResultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarMembresias", insertResultado.ActionName);

                Console.WriteLine("--- Insercion de membresia --- \nEjecución exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecución fallida.");
            }
            /* si resultado es diferente a nulo */
            if (updateResultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarMembresias", updateResultado.ActionName);

                Console.WriteLine("--- Modificacion de membresia --- \nEjecución exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecución fallida.");
            }
            /* si resultado es diferente a nulo */
            if (deleteResultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarMembresias", deleteResultado.ActionName);

                Console.WriteLine("--- Eliminacion de membresia --- \nEjecución exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecución fallida.");
            }
        }
    }
}
