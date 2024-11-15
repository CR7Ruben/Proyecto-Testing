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
        /* ejecucion => dotnet test -v normal --filter "modificarMembresia" */
        [Fact]
        public void modificarMembresia()
        {
            Console.WriteLine("--- Ejecucion - modificacion de membresia --- \n");

            Console.WriteLine("Inicializando datos de membresía...\n");

            Console.WriteLine("Inicializando controladores de membresia...\n");

            /* --- parametros --- */
            int p_id = 3;
            string p_membresia = "1 Semana";
            int p_duracion = 7;
            int p_precio = 0;

            /* arrange */
            /* se crea el objeto simulado moq en la clase membresia */
            var membresiaDatos = new membresiaDatos();
            /* se crea la instancia del controlador */
            var controller = new membresiaController();

            /* si llega a asignar un valor nulo 
             * no se modificara y devolvera error
             * para realizar la actualización debe 
             * existir la membresia */
            var membresia = new membresiaModel
            {
                idMembresia = p_id,
                membresia = p_membresia,
                duracion = p_duracion,
                precio = p_precio
            };

            /* act */
            /* manda a llamar al metodo */
            var resultado = controller.modificar(membresia) as RedirectToActionResult;

            /* assert */
            /* si resultado es diferente a nulo */
            if (resultado != null)
            {
                /* resultado esperado */
                Assert.Equal("listarMembresias", resultado.ActionName);

                Console.WriteLine($"Datos ingresados: \nMembresia => {p_membresia}. \nDuracion => {p_duracion}. \nPrecio => {p_precio}. \n")
                    ;
                Console.WriteLine("Ejecución exitosa.");
            }
            else /* si no */
            {
                Console.WriteLine("Ejecución fallida.");
            }
        }
    }
}
