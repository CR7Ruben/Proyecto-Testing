using Microsoft.AspNetCore.Mvc;

using gimnasio.Models;
using gimnasio.Datos;

namespace gimnasio.Controllers
{
    public class clienteController : Controller
    {

        clienteDatos _clienteDatos = new clienteDatos();

        /* ----------- */
        /* LISTAR VIEW */
        /* ----------- */

        public IActionResult listarClientes()
        {

            var oLista = _clienteDatos.listar();
            return View(oLista);

        }

        /* ------------- */
        /* INSERTAR VIEW */
        /* ------------- */

        public IActionResult insertar()
        {

            return View();

        }

        /* -------------- */
        /* REGISTRARSE SP */
        /* -------------- */

        [HttpPost]
        public IActionResult insertar(clienteModel oCliente)
        {

            var respuesta = _clienteDatos.insertar(oCliente);

            if (respuesta)
                return RedirectToAction("listarClientes");
            else
                return View();

        }

        /* ------------------- */
        /* CONSULTAR VIEW Y SP */
        /* ------------------- */

        public IActionResult consultarCliente(int idCliente)
        {

            var cliente = _clienteDatos.consultar(idCliente);

            return View(cliente);

        }

        /* -------------- */
        /* MODIFICAR VIEW */
        /* -------------- */

        public IActionResult modificarCliente(int idCliente)
        {

            var ocliente = _clienteDatos.consultar(idCliente);

            return View(ocliente);

        }

        /* ------------ */
        /* MODIFICAR SP */
        /* ------------ */

        [HttpPost]
        public IActionResult modificarCliente(clienteModel oCliente)
        {

            if (!ModelState.IsValid)
                return View(oCliente);

            var respuesta = _clienteDatos.modificar(oCliente);

            if (respuesta)
                return RedirectToAction("listarClientes");
            else
                return View(oCliente);

        }

        /* ------------- */
        /* ELIMINAR VIEW */
        /* ------------- */

        public IActionResult eliminarCliente(int idCliente)
        {

            var ocliente = _clienteDatos.consultar(idCliente);

            return View(ocliente);

        }

        /* ----------- */
        /* ELIMINAR SP */
        /* ----------- */

        [HttpPost]
        public IActionResult eliminarCliente(clienteModel oCliente)
        {

            var respuesta = _clienteDatos.eliminar(oCliente.idCliente);

            if (respuesta)
                return RedirectToAction("listarClientes");
            else
                return View(oCliente);

        }

    }
}
