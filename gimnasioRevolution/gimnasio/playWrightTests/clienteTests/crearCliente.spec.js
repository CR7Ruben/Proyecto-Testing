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
    /* verifica que sea visible */
    await expect(nuevoCliente).toBeVisible();

    /* regresa verdadero */
    return true;
}

test('Implementación', async ({ page }) => {

    const resultado = await insertarCliente(
        page,
        'ninguna',          
        'Java',             
        'diiii',           
        '666',              
        'ninguna'           
    );

    
    expect(resultado).toBe(true);

});