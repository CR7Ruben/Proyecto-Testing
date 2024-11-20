const { test, expect } = require('@playwright/test');

/* función registrar administrador */
async function registrarAdministrador(page, usuario, contrasena, repertirContrasena) {
    try {
        /* página */
        await page.goto('http://localhost:5228/');

        /* formulario */
        await page.fill('input[name="usuario"]', usuario);
        await page.fill('input[name="contrasena"]', contrasena);
        await page.fill('input[name="repetirContrasena"]', repertirContrasena);

        /* submit */
        await page.click('button[type="submit"]');

        /* regresa verdadero */
        return true;
    } catch { /* si no */
        /* regresa fallido */
        return false;
    }
}

/* función validación de administrador */
async function validarAdministrador(page, usuario, contrasena) {
    try {
        /* página */
        await page.goto('http://localhost:5228/administrador/validacionAdministrador');

        /* formulario */
        await page.fill('input[name="usuario"]', usuario);
        await page.fill('input[name="contrasena"]', contrasena);

        /* submit */
        await page.click('button[type="submit"]');

        /* si no, regresa inserción exitosa */
        /* si la url es la misma */
        const url = page.url();
        if (url == "http://localhost:5228/administrador/validacionAdministrador") {
            /* regresa inserción fallida */
            return false;
        }

        /* regresa inserción exitosa */
        return true;
    } catch { /* si no */
        /* regresa inserción fallida */
        return false;
    }
}

/* ejecución: npx playwright test implementacionAdministrador.spec.js */
test('Implementación', async ({ page }) => {
    /* menú
     * 1 = registrar administrador 
     * 2 = validar administrador */
    var opcion = 2; /* <-- parametro a cambiar */
    /* menú de opciones */
    switch (opcion) {
        /* opción 1 */
        case 1:
            /* registrar administrador */
            const registrarAdmin = await registrarAdministrador(
                page,
                /* usuario */
                'test', /* <-- parametro a cambiar */
                /* contrasena */
                'admin', /* <-- parametro a cambiar */
                /* repetirContrasena */
                'admin' /* <-- parametro a cambiar */
            );
            /* validación administrador */
            const validacion = await validarAdministrador(
                page,
                /* usuario */
                'test', /* <-- parametro a cambiar en base al registro*/
                /* contrasena */
                'admin' /* <-- parametro a cambiar en base al registro*/
            );
            /* validación de resultado */
            if (validacion == true) {
                /* entonces */
                console.log("Administrador registrado correctamente");
            } else { /* si no */
                /* entonces */
                console.log("Administrador no ha sido registrado");
            }
            break;
        /* opción 2 */
        case 2:
            /* validación administrador */
            const validacionAdmin = await validarAdministrador(
                page,
                /* usuario */
                'juan_daniel', /* <-- parametro a cambiar en base al registro*/
                /* contrasena */
                'admin1' /* <-- parametro a cambiar en base al registro*/
            );
            /* validación de resultado */
            if (validacionAdmin == true) {
                /* entonces */
                console.log("Administrador registrado correctamente");
            } else { /* si no */
                /* entonces */
                console.log("Administrador no ha sido registrado");
            }
            break;
        default:
            console.log("Opcion no disponible");
            break;
    }
});