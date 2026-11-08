using GestionInventario.Dominio;

var mouse = new Producto("Mouse", "PER-001", "Periféricos", 5, 50);
mouse.Ingresar(50);

try
{
    mouse.Egresar(20);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

Console.WriteLine($"{mouse.Nombre}: {mouse.Cantidad} unidades");

//1) Egresar exactamente todo el stock.
try
{
    mouse.Egresar(30);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

Console.WriteLine($"{mouse.Nombre}: {mouse.Cantidad} unidades");


//2) Ingresar una cantidad negativa.
try
{
    mouse.Ingresar(-10);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

Console.WriteLine($"{mouse.Nombre}: {mouse.Cantidad} unidades");

//3) Crear un producto con nombre vacío.
try
{
    var vacio = new Producto("", "2", "NADA", 10, 20);

}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

// 4)Crear un producto con mínimo y máximo iguales.
try
{
    var iguales = new Producto("Iguales", "3", "NADA", 10, 10);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

// 5)Ingresar hasta superar el máximo y mostrar si SobreStock es verdadero.
var superarMaximo = new Producto("Maximo", "4", "NADA", 10, 20);

superarMaximo.Ingresar(30);
Console.WriteLine(superarMaximo.SobreStock);

// 6) Bajar el stock hasta el mínimo y mostrar si FaltaStock es verdadero
var superarMinimo = new Producto("Minimo", "5", "NADA", 10, 20);
superarMinimo.Ingresar(15);
superarMinimo.Egresar(10);
Console.WriteLine(superarMinimo.FaltaStock);


//Pruebas de inventario

var teclado = new Producto("Teclado", "PER-002", "Periféricos", 1, 30);
var parlante = new Producto("Parlante", "PER-003", "Periféricos", 1, 20);
var televisor = new Producto("Televisor", "PER-004", "Periféricos", 1, 20);

var inventario = new Inventario();

inventario.Agregar(teclado);
inventario.Agregar(parlante);
inventario.Agregar(televisor);

//Agregar dos productos y mostrar cuántos hay en inventario.Productos.Count.
Console.WriteLine($"Productos en el inventario: {inventario.Productos.Count}");

//Registrar un ingreso por ID y mostrar la cantidad nueva.
Console.WriteLine($"Esta es la cantidad de teclados antes de ingresar: {teclado.Cantidad}");
inventario.RegistrarIngreso("PER-002",5);
Console.WriteLine($"Esta es la cantidad de teclados luego de ingresar: {teclado.Cantidad}");

//Registrar un egreso por ID y mostrar la cantidad nueva (acá vas a confirmar que el bug está corregido).
inventario.RegistrarIngreso("PER-003",10);
Console.WriteLine($"Esta es la cantidad de parlantes antes de egresar: {parlante.Cantidad}");
inventario.RegistrarEgreso("PER-003",5);
Console.WriteLine($"Esta es la cantidad de parlantes luego de egresar: {parlante.Cantidad}");

//Eliminar un producto con stock 0 y verificar que el Count bajó.
inventario.Eliminar("PER-004");
Console.WriteLine($"Productos en el inventario: {inventario.Productos.Count}");

//Recorrer inventario.Productos con un foreach y mostrar nombre y cantidad de cada uno.
foreach (var producto in inventario.Productos)
{
    Console.WriteLine($"Nombre: {producto.Nombre} - Cantidad: {producto.Cantidad}");
}

//Agregar un producto con un ID repetido.
try
{
    var controlRemoto = new Producto("Control para televisor", "PER-002", "Periféricos", 1, 30);
    inventario.Agregar(controlRemoto);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

//Buscar un ID inexistente.
try
{
    inventario.BuscarPorId("noexiste");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

//Egresar más de lo que hay, a través del inventario.
try
{
    inventario.RegistrarEgreso("PER-003",100);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}

//Eliminar un producto con stock.
try
{
    inventario.Eliminar("PER-002");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error esperado: {ex.Message}");
}