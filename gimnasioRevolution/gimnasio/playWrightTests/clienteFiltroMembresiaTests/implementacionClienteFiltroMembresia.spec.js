const { test, expect } = require('@playwright/test');

/* función de contar membresía */
async function contarClientes(page) {
    try {
        /* esperamos a que cargue el contenido */
        await page.waitForSelector('tr.cliente-test');

        /* busca la etiqueta class */
        const clientes = await page.locator('tr.cliente-test');

        /* cuenta cuantas veces se repite */
        const count = await clientes.count();

        /* regresa el conteo */
        return count;
    } catch { /* si no */
        /* regresa nulo */
        return null;
    }
}

/* función para seleccionr membresía */
async function seleccionarMembresia(page, idMembresia) {
    try {
        /* localiza el dropdown */
        const dropdown = await page.locator('#membresia');

        /* busca todas las etiquetas <option> en la etiqueta <select> */
        const opcion = await dropdown.locator('option');
        /* validar si tiene la opción 
         * evaluando todo el contenido en <opction> */
        const opcionSeleccionada = await opcion.evaluateAll((options, id) =>
            /* options es un listado de <option> donde buscamos que sea igual 
             * el valor que contiene <option> a la que mandamos a la función */
            options.some((option) => option.value === id), idMembresia
        );

        /* si el resultado es falso */
        if (opcionSeleccionada == false) {
            /* entonces no existe */
            return false;
        }

        /* seleccionar la opción */
        await dropdown.selectOption({ value: idMembresia });

        /* esperar que las filas de clientes se actualice */
        await page.waitForSelector('tr.cliente-test');

        /* si todo sale bien */
        return true;
    } catch { /* si no */
        /* regresa false */
        return false;
    }
}

test("Implementación", async ({ page }) => {
    /* página */
    await page.goto('http://localhost:5228/clientePorMembresia/Listar');

    /* clientes con membresía asignada */
    const conteoClientes = await contarClientes(page);
    console.log(`Clientes con membresía asignada: ${conteoClientes}`);

    /* filtro */
    const idMembresia = '1026'; /* <-- parametro a cambiar */
    const filtro = await seleccionarMembresia(page, idMembresia);
    if (filtro == true) {
        /* conteo final */
        const conteoClientesFinal = await contarClientes(page);
        console.log(`Clientes después de aplicar filtro: ${conteoClientesFinal}`);
    } else {
        console.log("No se pudo aplicar")
    }
}); 
