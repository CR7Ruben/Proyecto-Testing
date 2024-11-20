const { test, expect } = require('@playwright/test');

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

/* función para buscar membresías */
async function buscarMembresiaId(page, idMembresia) {
    /* intenta */
    try {
        /* página */
        await page.goto('http://localhost:5228/membresia/listarMembresias');

        /* seleciconara el id exacto */
        const membresia = await page.locator(`tr.membresia-test:has(td.idMembresia-test:has-text("${idMembresia}"))`);

        /* si id no existe */
        if (await membresia.count() == 0) {
            /* cliente no encontrado */
            return null;
        }

        /* buscamos los clientes */
        const resultado = await membresia.locator('.idMembresia-test').textContent();

        /* encontrado */
        return resultado;
    } catch { /* si no */
        /* no encontrado */
        return null;
    }
}

/* función para asignar membresía */
async function asignarMembresia(page, idCliente, idMembresia) {
    /* intenta */
    try {
        /* página */
        await page.goto(`http://localhost:5228/detallesCliente/agregarMembresia?idCliente=${idCliente}`);

        /* asignamos la membresía desde el dropdown */
        const dropdown = await page.locator('#idMembresia');
        /* seleccionamos la opción */
        await dropdown.selectOption({ value: idMembresia.toString() });

        /* submit */
        await page.click('button[type="submit"]');

        /* se asigno */
        return true;
    } catch { /* si no */
        /* no se asigno */
        return false;
    }
}

/* función para actualizar membresía */
async function actualizarMembresia(page, idCliente, idMembresia) {
    /* intenta */
    try {
        /* página */
        await page.goto(`http://localhost:5228/detallesCliente/actualizarMembresia?idCliente=${idCliente}`);

        /* asignamos la membresía desde el dropdown */
        const dropdown = await page.locator('#idMembresia');
        /* seleccionamos la opción */
        await dropdown.selectOption({ value: idMembresia.toString() });

        /* submit */
        await page.click('button[type="submit"]');

        /* se asigno */
        return true;
    } catch { /* si no */
        /* no se asigno */
        return false;
    }
}

/* ejecución: npx playwright test implementacionAsignacion.spec.js */
test('Implementación', async ({ page }) => {
    /* buscamos al cliente */
    const cliente = await buscarClienteId(page, '4062'); /* <-- parametro a cambiar por cliente existente*/
    /* validación de busqueda */
    if (cliente != null) {
        /* entonces */
        console.log(`cliente encontrado: ${cliente}`);
    } else { /* si no */
        /* entonces */
        console.log("Cliente no encontrado");
    }

    /* buscamos la membresía */
    const membresia = await buscarMembresiaId(page, '1027'); /* <-- parametro a cambiar por membresía existente*/
    /* validación de busqueda */
    if (membresia != null) {
        /* entonces */
        console.log(`Membresia encontrada: ${membresia}`);
    } else { /* si no */
        /* entonces */
        console.log("Membresia no encontrada");
    }

    /* menú
     * 1 = asiganr
     * 2 = actualizar */
    var opcion = 2;
    /* menú de opciones */
    switch (opcion) {
        /* opción 1 */
        case 1:
            /* si ambos existen */
            if (cliente != null && membresia != null) {
                /* asignar membresía */
                const asignacion = await asignarMembresia(
                    page,
                    cliente,
                    membresia
                );
                /* validación de la asignación */
                if (asignacion == true) {
                    console.log("Asignacion realizada");
                } else { /* si no */
                    console.log("No se fue posible asignar la membresia");
                }
            } else { /* si no */
                console.log("No se fue posible asignar la membresia");
            }
            break;
        /* opción 2 */
        case 2:
            /* si ambos existen */
            if (cliente != null && membresia != null) {
                /* asignar membresía */
                const asignacion = await actualizarMembresia(
                    page,
                    cliente,
                    membresia
                );
                /* validación de la asignación */
                if (asignacion == true) {
                    console.log("Actualizacion realizada");
                } else { /* si no */
                    console.log("No se fue posible actualizar la membresia");
                }
            } else { /* si no */
                console.log("No se fue posible actualizar la membresia");
            }
            break;
        /* default */
        default:
            console.log("Opcion no existente");
            break;
    }
});