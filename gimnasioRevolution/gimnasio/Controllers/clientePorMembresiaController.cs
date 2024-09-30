using Microsoft.AspNetCore.Mvc;
using gimnasio.Datos;
using gimnasio.Models;
using System.Data.SqlClient;
using System.Data;

namespace gimnasio.Controllers
{
    public class clientePorMembresiaController : Controller
    {

        clientePorMembresiaDatos _clientePorMembresiaDatos = new clientePorMembresiaDatos();

        membresiaDatos _membresiaDatos = new membresiaDatos();

        [HttpGet]
        public IActionResult Listar()
        {
            var membresia = _membresiaDatos.ListarMembresias();
            ViewBag.membresia = membresia;
            ViewBag.membresiaSeleccionada = 0;

            List<clientePorMembresiaModel> oLista = _clientePorMembresiaDatos.Listar();
            return View(oLista);
        }

        [HttpPost]
        public IActionResult Listar(int membresia)
        {
            var membresiaList = _membresiaDatos.ListarMembresias();
            ViewBag.membresia = membresiaList;
            ViewBag.membresiaSeleccionada = membresia;

            List<clientePorMembresiaModel> oLista = membresia == 0
                ? _clientePorMembresiaDatos.Listar()
                : _clientePorMembresiaDatos.ListarPorMembresia(membresia);

            return View(oLista);
        }

    }
}
