using GestionInventario.Dominio;

namespace GestionInventario.Presentacion;

public class MenuConsola
{
    private readonly Inventario _inventario;

    public MenuConsola(Inventario inventario)
    {
        _inventario = inventario;
    }

    public void Ejecutar()
    {
        bool seguir = true;

        while (seguir)
        {
            Console.Clear();
            MostrarOpciones();
            Console.Write("Elegí una opción: ");
            string? opcion = Console.ReadLine();

            try
            {
                switch (opcion)
                {
                    case "1":
                        ListarProductos();
                        break;
                    case "2":
                        AgregarProducto();
                        break;
                    case "0":
                        seguir = false;
                        break;
                    default:
                        MostrarError("Opción inválida");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                MostrarError(ex.Message);
            }

            if (seguir)
            {
                Pausar();
            }
        }
    }

    private void MostrarOpciones()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("      SISTEMA DE GESTIÓN DE INVENTARIO   ");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("1-Listar productos");
        Console.WriteLine("2-Agregar producto");
        Console.WriteLine("3-Consultar producto por ID");
        Console.WriteLine("4-Registrar ingreso de stock");
        Console.WriteLine("5-Registrar egreso de stock");
        Console.WriteLine("6-Eliminar producto");
        Console.WriteLine("7-Ver alertas de stock");
        Console.WriteLine("0-Salir");
    }

    private void ListarProductos()
    {
        if (_inventario.Productos.Count == 0)
        {
            Console.WriteLine("No hay productos cargados");
            return;
        }
        foreach (var producto in _inventario.Productos)
        {
            Console.WriteLine($"ID: {producto.Id} | Nombre: {producto.Nombre} | Categoría: {producto.Categoria} | Cantidad: {producto.Cantidad}");
        }
    }
    private void AgregarProducto()
    {
        string nombre, id, categoria;
        int stockMax, stockMin;

        Console.Write("Ingrese nombre del producto: ");
        nombre = Console.ReadLine();
        Console.Write("Ingrese ID del producto: ");
        id = Console.ReadLine();
        Console.Write("Ingrese categoria del producto: ");
        categoria = Console.ReadLine();
        stockMin = LeerEntero("Stock mínimo: ");
        stockMax = LeerEntero("Stock máximo: ");

        var producto = new Producto(nombre, id, categoria, stockMin, stockMax);
        _inventario.Agregar(producto);
        MostrarExito($"Producto {producto.Id} agregado correctamente.");

    }

    private int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? texto = Console.ReadLine();

            if (int.TryParse(texto, out int numero))
            {
                return numero;
            }

            MostrarError("Debe ingresar un número válido.");
        }
    }

    private void MostrarExito(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }

    private void MostrarError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(mensaje);
        Console.ResetColor();
    }

    private void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine("Presioná una tecla para volver al menú...");
        Console.ReadKey(true);
    }
}