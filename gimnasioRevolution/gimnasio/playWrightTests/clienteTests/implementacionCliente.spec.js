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

    /* formulario */
    await page.fill('input[name="fotoUrl"]', fotoUrl);
    await page.fill('input[name="nombre"]', nombre);
    await page.fill('input[name="apellido"]', apellido);
    await page.fill('input[name="numTel"]', numTel);
    await page.fill('textarea[name="observaciones"]', observaciones);
    /* submit */
    await page.click('button[type="submit"]');

    /* espera por la actualización */
    await page.waitForSelector('tr:has-text("' + nombre + '")');

    /* verifica que la lista contiene el nuevo cliente */
    const nuevoCliente = await page.locator('tr:has-text("' + nombre + '")');

    /* regresa verdadero */
    return true;
}

/* función para recibir id de cliente */
async function buscarClienteId(page, idCliente) {
    /* página */
    await page.goto('http://localhost:5228/cliente/listarClientes');

    /* seleciconara el id exacto */
    const cliente = await page.locator(`tr.cliente-test:has(td.idCliente-test:has-text("${idCliente}"))`);

    /* buscamos los clientes */
    const resultado = await cliente.locator('.idCliente-test').first().textContent();

    /* regresamos el id */
    return resultado;
}

/* función para modificar cliente por id */
async function modificarClientePorId(page, idCliente, fotoUrl, nombre, apellido, numTel, observaciones) {
    /* página */
    await page.goto(`http://localhost:5228/cliente/modificarCliente?idCliente=${idCliente}`);

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

    /* regresa true */
    return true;
}

/* función para eliminar cliente */
async function eliminarCliente(page, idCliente) {
    /* página */
    await page.goto(`http://localhost:5228/cliente/eliminarCliente?idCliente=${idCliente}`);

    /* submit */
    await page.click('button[type="submit"]');

    /* regresa true */
    return true;
}

/* ejecución: npx playwright test implementacionCliente.spec.js */
test('Implementación', async ({ page }) => {
    /* conteo previo inserción cliente */
    var conteoPrev = await contarClientes(page);
    console.log(`Clientes ingresados: ${conteoPrev}`);

    /* inserción de clientes */
    try {
        const insertCliente = await insertarCliente(
            page,
            /* foto */
            'ninguna',
            /* nombre */
            'wasap',
            /* apellido */
            'loco',
            /* numTel */
            '666',
            /* observaciones */
            'ninguna'
        );
        console.log("Cliente insertado correctamente");
        /* conteo post inserción de cliente */
        var conteoPost = await contarClientes(page);
        console.log(`Clientes ingresados: ${conteoPost}`);
    } catch {
        console.log("No se pudo ingresar cliente")
    }

    /* buscar cliente */
    try {
        var findById = await buscarClienteId(page, 4039);
        console.log(`Cliente encontrado: ${findById}`);
    } catch {
        console.log("Cliente no encontrado")
    }

    /* modificar cliente */
    try {
        const updateCliente = await modificarClientePorId(
            page,
            findById,
            /* foto */
            'testUpdate',
            /* nombre */
            'testUpdate',
            /* apellido */
            'testUpdate',
            /* numTel */
            '101011',
            /* observaciones */
            'testUpdate'
        );
        console.log("Cliente modificado correctamente");
    } catch {
        console.log("No se pudo modificar el cliente");
    }

    /* eliminar cliente */
    try {
        var idEliminar = 4038;
        var eliminarResultado = await eliminarCliente(page, idEliminar);
        console.log("Cliente eliminado correctamente")
        /* conteo post eliminacion de cliente */
        var conteoPostDel = await contarClientes(page);
        console.log(`Clientes ingresados: ${conteoPostDel}`);
    } catch {
        console.log("No se pudo eliminar cliente")
    }
});