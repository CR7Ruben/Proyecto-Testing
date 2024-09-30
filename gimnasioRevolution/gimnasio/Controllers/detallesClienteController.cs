using Microsoft.AspNetCore.Mvc;
using gimnasio.Datos;
using gimnasio.Models;
using System.Data.SqlClient;
using System.Data;

namespace gimnasio.Controllers
{

    public class detallesClienteController : Controller
    {

        detallesClienteDatos _detallesClienteDatos = new detallesClienteDatos();

        membresiaDatos _membresiaDatos = new membresiaDatos();

        public IActionResult agregarMembresia(int idCliente)
        {

            var membresias = _membresiaDatos.ListarMembresias();
            ViewBag.Membresias = membresias;

            var cliente = _detallesClienteDatos.consultar(idCliente);

            var model = new detallesClienteModel
            {
                idCliente = cliente.idCliente,
                idMembresia = cliente.idMembresia

            };

            return View(model);

        }

        /* -------------- */
        /* REGISTRARSE SP */
        /* -------------- */

        [HttpPost]
        public IActionResult agregarMembresia(detallesClienteModel odetallesCliente)
        {

            var membresias = _membresiaDatos.ListarMembresias();
            ViewBag.Membresias = membresias;

            var respuesta = _detallesClienteDatos.insertar(odetallesCliente);

            if (respuesta)
                return RedirectToAction("listarClientes", "cliente");
            else
                return View();

        }



        public IActionResult actualizarMembresia(int idCliente)
        {

            var membresias = _membresiaDatos.ListarMembresias();
            ViewBag.Membresias = membresias;

            var cliente = _detallesClienteDatos.consultar(idCliente);

            var model = new detallesClienteModel
            {
                idCliente = cliente.idCliente,
                idMembresia = cliente.idMembresia

            };

            return View(model);

        }

        /* -------------- */
        /* REGISTRARSE SP */
        /* -------------- */

        [HttpPost]
        public IActionResult actualizarMembresia(detallesClienteModel odetallesCliente)
        {

            var membresias = _membresiaDatos.ListarMembresias();
            ViewBag.Membresias = membresias;

            var respuesta = _detallesClienteDatos.modificar(odetallesCliente);

            if (respuesta)
                return RedirectToAction("ListarClientes", "Cliente");
            else
                return View();

        }

    }

}
