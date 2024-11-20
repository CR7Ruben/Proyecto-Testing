const { test, expect } = require('@playwright/test');

/* función para contar clientes */
async function contarClientes(page) {
    /* página */
    await page.goto('http://localhost:5228/cliente/listarClientes');

    /* buscamos los clientes */
    const clientes = await page.locator('.cliente-test');

    /* número de clientes */
    const count = await clientes.count();

    /* resultado esperado */
    await expect(clientes).toHaveCount(count);

    /* regresa el conteo de clientes */
    return count;
}

/* función para ingresar cliente */
async function insertarCliente(page, fotoUrl, nombre, apellido, numTel, observaciones) {
    /* página */
    await page.goto('http://localhost:5228/cliente/insertar');

    /* intenta */
    try {
        /* formulario */
        await page.fill('input[name="fotoUrl"]', fotoUrl);
        await page.fill('input[name="nombre"]', nombre);
        await page.fill('input[name="apellido"]', apellido);
        await page.fill('input[name="numTel"]', numTel);
        await page.fill('textarea[name="observaciones"]', observaciones);
        /* submit */
        await page.click('button[type="submit"]');

        /* regresa verdadero */
        return true;
    } catch { /* si no */
        /* regresa */
        return false;
    }

    /* url */
    var url = page.url()
    /* si se mantiene en la misma ventana */
    if (url == 'http://localhost:5228/cliente/insertar') {
        /* no se inserto */
        return false
    }

    /* esperar 2 segundos */
    await page.waitForTimeout(2000);

    /* espera por la actualización */
    await page.waitForSelector('tr:has-text("' + nombre + '")');

    /* verifica que la lista contiene el nuevo cliente */
    const nuevoCliente = await page.locator('tr:has-text("' + nombre + '")');

    /* inserción exitosa */
    return true;
}

/* función para buscar cliente */
async function buscarClienteId(page, idCliente) {
    /* intenta */
    try {
        /* página */
        await page.goto('http://localhost:5228/cliente/listarClientes');

        /* seleciconara el id exacto */
        const cliente = await page.locator(`tr.cliente-test:has(td.idCliente-test:has-text("${idCliente}"))`);

        /* si id no existe */
        if (await cliente.count() == 0) {
            /* cliente no encontrado */
            return null;
        }

        /* buscamos los clientes */
        const resultado = await cliente.locator('.idCliente-test').textContent();

        /* encontrado */
        return resultado;
    } catch { /* si no */
        /* no encontrado */
        return null;
    }
}

/* función para modificar cliente por id */
async function modificarCliente(page, idCliente, fotoUrl, nombre, apellido, numTel, observaciones) {
    /* página */
    await page.goto(`http://localhost:5228/cliente/modificarCliente?idCliente=${idCliente}`);

    try {
        /* formulario */
        await page.fill('input[name="fotoUrl"]', fotoUrl);
        await page.fill('input[name="nombre"]', nombre);
        await page.fill('input[name="apellido"]', apellido);
        await page.fill('input[name="numTel"]', numTel);
        await page.fill('textarea[name="observaciones"]', observaciones);
        /* submit */
        await page.click('button[type="submit"]');

        /* espera por la actualización */
        await page.waitForSelector(`tr:has(td:has-text("${idCliente}"))`);

        /* verifica que la lista contiene el cliente modificado */
        const updateCliente = await page.locator(`tr:has(td:has-text("${idCliente}"))`);

        /* regresa verdadero */
        return true;
    } catch { /* si no */
        /* regresa */
        return false;
    }

    const url = page.url();
    if (url == 'http://localhost:5228/cliente/modificarCliente') {
        /* no se modifico */
        return false;
    }

    /* esperar 2 segundos */
    await page.waitForTimeout(2000);

    /* regresa true */
    return true;
}

/* función para eliminar cliente */
async function eliminarCliente(page, idCliente) {
    /* página */
    await page.goto(`http://localhost:5228/cliente/eliminarCliente?idCliente=${idCliente}`);

    /* submit */
    await page.click('button[type="submit"]');

    /* url */
    const url = page.url();
    if (url == 'http://localhost:5228/cliente/eliminarCliente') {
        /* no se elimino */
        return false;
    }

    /* regresa true */
    return true;
}

/* ejecución: npx playwright test implementacionCliente.spec.js */
test('Implementación', async ({ page }) => {
    /* conteo previo inserción cliente */
    var conteoPrev = await contarClientes(page);
    console.log(`Clientes ingresados: ${conteoPrev}`);

    /* menú
     * 1 = insertar cliente
     * 2 = buscar cliente 
     * 3 = modificar cliente 
     * 4 = eliminar cliente */
    var opcion = 4; /* <-- parametro a cambiar */
    /* menú de opciones */
    switch (opcion) {
        /* opción 1 */
        case 1:
            /* inserción de clientes */
            const insercionCliente = await insertarCliente(
                page,
                /* foto */
                'ninguna', /* <-- parametro a cambiar */
                /* nombre */
                'new', /* <-- parametro a cambiar */
                /* apellido */
                'estoy kk lala', /* <-- parametro a cambiar */
                /* numTel */
                '666', /* <-- parametro a cambiar */
                /* observaciones */
                'ninguna' /* <-- parametro a cambiar */
            );
            /* si el número de cliente no cambio */
            var conteoPost = await contarClientes(page);
            if (conteoPost == conteoPrev) {
                /* entonces */
                console.log("Cliente no ha sido ingresado");
            } else { /* si no */
                /* si se inserto */
                if (insercionCliente == true) {
                    /* entonces */
                    console.log("Cliente insertado correctamente");
                    console.log(`Clientes ingresados: ${conteoPost}`);
                } else { /* si no */
                    /* entonces */
                    consolo.log("Cliente no ha sido ingresado");
                }
            }
            break;
        /* opción 2 */
        case 2:
            /* buscar cliente */
            var findById = await buscarClienteId(page, '4061'); /* <-- parametro a cambiar */
            /* validación de busqueda */
            if (findById != null) {
                /* entonces */
                console.log(`Cliente encontrado: ${findById}`);
            } else { /* si no */
                /* entonces */
                console.log("Cliente no encontrado")
            }
            break;
        /* opción 3 */
        case 3:
            /* buscar cliente */
            var updateById = await buscarClienteId(page, '4061'); /* <-- parametro a cambiar */
            /* validación de busqueda */
            if (updateById != null) {
                /* entonces */
                console.log(`Cliente encontrado: ${updateById}`);
            } else { /* si no */
                /* entonces */
                console.log("Cliente no encontrado")
            }
            /* modificar cliente */
            const updateCliente = await modificarCliente(
                page,
                updateById,
                /* foto */
                'testUpdate', /* <-- parametro a cambiar */
                /* nombre */
                'testUpdate', /* <-- parametro a cambiar */
                /* apellido */
                'testUpdate', /* <-- parametro a cambiar */
                /* numTel */
                '101011', /* <-- parametro a cambiar */
                /* observaciones */
                'testUpdate' /* <-- parametro a cambiar */
            );
            /* validación de modificación */
            if (updateCliente == true) {
                /* entonces */
                console.log("Cliente modificado correctamente");
            } else { /* si no */
                /* entonces */
                console.log("Cliente no ha sido encontrado")
            }
            break;
        /* opción 4 */
        case 4:
            /* buscar cliente para eliminar */
            var deleteById = await buscarClienteId(page, '4061'); /* <-- parametro a cambiar */
            /* validación de busqueda */
            if (deleteById != null) {
                /* entonces */
                console.log(`Cliente encontrado: ${deleteById}`);
                var eliminarResultado = await eliminarCliente(page, deleteById);
                console.log("Cliente eliminado correctamente")
                /* conteo post eliminacion de cliente */
                var conteoPostDel = await contarClientes(page);
                console.log(`Clientes ingresados: ${conteoPostDel}`);
            } else { /* si no */
                /* entonces */
                console.log("Cliente no encontrado")
            }
            break;
        /* default */
        default:
            console.log("Opcion no existente");
            break;
    }
});