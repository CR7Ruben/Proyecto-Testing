using gimnasio.Datos;
using Microsoft.AspNetCore.Mvc;

namespace gimnasio.Controllers
{
    public class viewDetallesClienteController : Controller
    {

        viewDetallesClienteDatos _viewDetallesClienteDatos = new viewDetallesClienteDatos();

        /* ------------------- */
        /* CONSULTAR VIEW Y SP */
        /* ------------------- */

        public IActionResult consultarviewDetallesCliente(int idCliente)
        {

            var viewDetallesCliente = _viewDetallesClienteDatos.consultar(idCliente);

            return View(viewDetallesCliente);

        }

    }
}
