using Microsoft.AspNetCore.Mvc;
using gimnasio.Datos;
using gimnasio.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Data.SqlClient;
using System.Data;

namespace gimnasio.Controllers
{
    public class membresiaController : Controller
    {

        membresiaDatos _membresiaDatos = new membresiaDatos();

        /* ---------------- */
        /* REGISTRARSE VIEW */
        /* ---------------- */

        public IActionResult listarMembresias()
        {
            var oLista = _membresiaDatos.listarMembresias();
            return View(oLista);
        }


        public IActionResult agregar()
        {

            return View();

        }

        [HttpPost]
        public IActionResult agregar(membresiaModel omembresia)
        {

            var respuesta = _membresiaDatos.agregar(omembresia);

            if (respuesta)
                return RedirectToAction("listarMembresias");
            else
                return View();

        }

        public IActionResult consultar(int idMembresia)
        {

            var omembresia = _membresiaDatos.consultar(idMembresia);

            return View(omembresia);

        }

        public IActionResult modificar(int idMembresia)
        {

            var omembresia = _membresiaDatos.consultar(idMembresia);

            return View(omembresia);

        }

        [HttpPost]
        public IActionResult modificar(membresiaModel omembresia)
        {

            if (!ModelState.IsValid)
                return View(omembresia);

            var respuesta = _membresiaDatos.modificar(omembresia);

            if (respuesta)
                return RedirectToAction("listarMembresias");
            else
                return View(omembresia);

        }

        public IActionResult eliminar(int idMembresia)
        {

            var omembresia = _membresiaDatos.consultar(idMembresia);

            return View(omembresia);

        }


        [HttpPost]
        public IActionResult eliminar(membresiaModel omembresia)
        {

            var respuesta = _membresiaDatos.eliminar(omembresia.idMembresia);

            if (respuesta)
                return RedirectToAction("listarMembresias");
            else
                return View(omembresia);

        }

    } 

}
