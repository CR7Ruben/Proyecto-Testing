using Microsoft.AspNetCore.Mvc;
using gimnasio.Datos;
using gimnasio.Models;

namespace gimnasio.Controllers
{

    public class administradorController : Controller
    {

        administradorDatos _administradorDatos = new administradorDatos();

        /* ---------------- */
        /* REGISTRARSE VIEW */
        /* ---------------- */

        public IActionResult registrarAdministrador()
        {

            return View();

        }

        /* ------------------- */
        /* PROCESO REGISTRARSE */
        /* ------------------- */

        [HttpPost]
        public IActionResult registrarAdministrador(administradorModel oadministrador)
        {

            var respuesta = _administradorDatos.registrarAdministrador(oadministrador);

            if (respuesta)

                return RedirectToAction("validacionAdministrador");

            else

                return View(oadministrador);

        }

        /* --------------- */
        /* VALIDACIÓN VIEW */
        /* --------------- */

        public IActionResult validacionAdministrador()
        {

            return View();

        }

        /* ------------------ */
        /* PROCESO VALIDACIÓN */
        /* ------------------ */

        [HttpPost]
        public IActionResult validacionAdministrador(administradorModel oadministrador)
        {
            var idAdministrador = _administradorDatos.validacionAdministrador(oadministrador);

            if (idAdministrador.HasValue)
            
                return RedirectToAction("home");
            
            else
                
                return View(oadministrador);
            
        }

        /* --------- */
        /* HOME VIEW */
        /* --------- */

        public IActionResult home()
        {

            return View();

        }

    }

}
