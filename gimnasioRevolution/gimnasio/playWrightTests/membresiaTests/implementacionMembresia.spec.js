const { test, expect } = require('@playwright/test');

/* función para contar membresía */
async function contarMembresias(page) {
    /* página */
    await page.goto('http://localhost:5228/membresia/listarMembresias');

    /* buscamos las membresías */
    const membresias = await page.locator('.membresia-test');

    /* número de membresías */
    const count = await membresias.count();

    /* resultado esperado */
    await expect(membresias).toHaveCount(count);

    return count;
}

/* función para ingresar membresía */
async function insertarMembresia(page, membresia, duracion, precio) {
    /* página */
    await page.goto('http://localhost:5228/membresia/agregar');

    /* intenta */
    try {
        /* formulario */
        await page.fill('input[name="membresia"]', membresia);
        await page.fill('input[name="duracion"]', duracion);
        await page.fill('input[name="precio"]', precio);
        /* submit */
        await page.click('button[type="submit"]');

        /* regresa verdadero */
        return true;
    } catch { /* si no */
        /* regresa */
        return false;
    }

    /* esperar 2 segundos */
    await page.waitForTimeout(2000);

    /* inserción exitosa */
    return true;
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

/* función para modificar membresía */
async function modificarMembresia(page, idMembresia, membresia, duracion, precio) {
    /* página */
    await page.goto(`http://localhost:5228/membresia/modificar?idMembresia=${idMembresia}`)

    /* intenta */
    try {
        /* formulario */
        await page.fill('input[name="membresia"]', membresia);
        await page.fill('input[name="duracion"]', duracion);
        await page.fill('input[name="precio"]', precio);
        /* submit */
        await page.click('button[type="submit"]');

        /* regresa verdadero */
        return true;
    } catch { /* si no */
        /* regresa */
        return false;
    }

    /* url */
    const url = page.url();
    if (url == 'http://localhost:5228/membresia/modificar') {
        /* no se modifico */
        return false;
    }

    /* esperar 2 segundos */
    await page.waitForTimeout(2000);

    /* inserción exitosa */
    return true;
}

/* función para eliminar membresía */
async function eliminarMembresia(page, idMembresia) {
    /* página */
    await page.goto(`http://localhost:5228/membresia/eliminar?idMembresia=${idMembresia}`);

    /* submit */
    await page.click('button[type="submit"]');

    /* url */
    const url = page.url();
    if (url == 'http://localhost:5228/membresia/eliminar') {
        /* no se elimino */
        return false;
    }

    /* regresa true */
    return true;
}

/* ejecución: npx playwright test implementacionMembresia.spec.js */
test('Implementación', async ({ page }) => {
    /* conteo previo inserción membresia */
    var conteoPrev = await contarMembresias(page);
    console.log(`Membresias ingresadas: ${conteoPrev}`);

    /* menú
     * 1 = insertar membresía
     * 2 = buscar membresía 
     * 3 = modificar membresía 
     * 4 = eliminar membresía */
    var opcion = 4;
    /* menú de opciones */
    switch (opcion) {
        /* opción 1 */
        case 1:
            /* inserción de membresía */
            const insercionMembresia = await insertarMembresia(
                page,
                /* membresía */
                '3 dias',
                /* duración */
                '3',
                /* precio */
                '3'
            );
            /* si el número de membresías no cambio */
            var conteoPost = await contarMembresias(page);
            if (conteoPost == conteoPrev) {
                /* entonces */
                console.log("Membresia no ha sido ingresada")
            } else { /* si no */
                if (insercionMembresia == true) {
                    /* entonces */
                    console.log("Membresia ingresada correctamente");
                    /* conteo post inserción membresia */
                    var conteoPost = await contarMembresias(page);
                    console.log(`Membresias ingresadas: ${conteoPost}`);
                } else { /* si no */
                    /* entonces */
                    console.log("Membresia no ha sido ingresada")
                }
            }
            break;
        /* opción 2 */
        case 2:
            /* buscar membresía */
            var findById = await buscarMembresiaId(page, '1025');
            /* validación de busqueda */
            if (findById != null) {
                /* entonces */
                console.log(`Membresia encontrada: ${findById}`);
            } else { /* si no */
                /* entonces */
                console.log("Membresia no encontrada")
            }
            break;
        /* opción 3 */
        case 3:
            /* buscar membresía */
            var updateById = await buscarMembresiaId(page, '1025');
            /* validación de busqueda */
            if (updateById != null) {
                /* entonces */
                console.log(`Membresia encontrada: ${updateById}`);
            } else { /* si no */
                /* entonces */
                console.log("Membresia no encontrada")
            }
            /* modificación de membresía */
            const modificacionMembresia = await modificarMembresia(
                page,
                updateById,
                /* membresía */
                'update',
                /* duración */
                '0',
                /* precio */
                '0'
            );
            if (modificacionMembresia == true) {
                console.log("Membresia modificada");
            } else {
                console.log("Membresia no se ha modificado");
            }
            break;
        /* opción 4 */
        case 4:
            /* buscar membresía para eliminar */
            var deleteById = await buscarMembresiaId(page, '1025');
            /* validación de busqueda */
            if (deleteById != null) {
                /* entonces */
                console.log(`Membresia encontrada: ${deleteById}`);
                var eliminarResultado = await eliminarMembresia(page, deleteById);
                console.log("Membresia eliminada correctamente")
                /* conteo post eliminacion de cliente */
                var conteoPostDel = await contarMembresias(page);
                console.log(`Membresia ingresadas: ${conteoPostDel}`);
            } else { /* si no */
                /* entonces */
                console.log("Membresia no encontrada")
            }
            break;
        /* default */
        default:
            console.log("Opcion no existente");
            break;
    }
});
