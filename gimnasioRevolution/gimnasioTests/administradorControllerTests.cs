using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using gimnasio.Controllers;
using gimnasio.Datos;
using gimnasio.Models;

namespace gimnasioTests
{
    public class administradorControllerTests
    {

        [Fact]
        public void homeAdministradorTest()
        {

            /* arrange */
            /* se crea la instancia del controlador */
            administradorController controller = new administradorController();

            /* act */
            /* se manda a llamar al metodo home y
             * resultado sera una view */
            ViewResult resultado = controller.home() as ViewResult;

            /* assert */
            /* confirma que resultado no esta vacio,
             * confirmando que devolvio una vista correctamente*/
            Assert.NotNull(resultado);

        }

        [Fact]
        public void registrarAdministradorTest()
        {

            /* rrange */
            /* se crea el objeto simulado moq en la clase administrador */
            var mockAdministradorDatos = new Mock<administradorDatos>();
            /* se crea la instancia del controlador */
            administradorController controller = new administradorController();

            /* usa el modelo */
            var administrador = new administradorModel
            {
                usuario = "juan_daniel",
                contrasena = "admin1",
                repetirContrasena = "admin1"
            };

            /* act */
            /* manda a llamar al metodo registrarAdministrador y
             * verifica que el resultado devuelve una vista */
            var resultado = controller.registrarAdministrador(administrador) as RedirectToActionResult;

            /* assert */
            /* verifica que no este vacio y confirma si devolvio una view correctamente */
            Assert.NotNull(resultado);
            /* resultado esperado seria la vista "validacionAdministrador" */
            Assert.Equal("validacionAdministrador", resultado.ActionName);

        }

        [Fact]
        public void validacionAdministradorTest()
        {

            /* arrange */
            /* se crea el objeto simulado moq en la clase administrador */
            var mockAdministradorDatos = new Mock<administradorDatos>();
            /* se crea la instancia del controlador */
            var controller = new administradorController();

            /* usa el modelo */
            var administrador = new administradorModel
            {
                usuario = "juan_daniel",
                contrasena = "admin1",

            };

            /* act */
            /* manda a llamar al metodo validacionAdministrador y
             * verifica que el resultado devuelve una vista */
            var resultado = controller.validacionAdministrador(administrador) as RedirectToActionResult;

            /* assert */
            /* verifica que no este vacio y confirma si devolvio una view correctamente */
            Assert.NotNull(resultado);
            /* resultado esperado seria la vista "validacionAdministrador" */
            Assert.Equal("home", resultado.ActionName);

        }

    }
}