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


