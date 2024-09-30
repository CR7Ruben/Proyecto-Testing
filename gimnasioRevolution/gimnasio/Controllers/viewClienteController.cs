using gimnasio.Datos;
using Microsoft.AspNetCore.Mvc;

namespace gimnasio.Controllers
{

    public class viewClienteController : Controller
    {

        viewClienteDatos _viewClienteDatos = new viewClienteDatos();

        /* ----------- */
        /* LISTAR VIEW */
        /* ----------- */

        public IActionResult listarviewCliente()
        {

            var oLista = _viewClienteDatos.listar();
            return View(oLista);

        }

    }

}
