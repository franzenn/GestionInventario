namespace GestionInventario.Dominio;

public class Inventario
{
    private readonly List<Producto> _productos = new();

    public IReadOnlyList<Producto> Productos => _productos;

    public bool Existe(string id)
    {
        foreach (var producto in _productos)
        {
            if (producto.Id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void Agregar(Producto producto)
    {
        if (Existe(producto.Id))
        {
            throw new InvalidOperationException($"Ya existe un producto con este ID: {producto.Id}");
        }
        _productos.Add(producto);
    }

    public Producto BuscarPorId(string id)
    {
        foreach (var producto in _productos)
        {

            if (producto.Id == id)
            {
                return producto;
            }
        }
        throw new KeyNotFoundException($"No existe producto con un ID: {id}");
    }

    public void RegistrarIngreso(string id, int cantidad)
    {
        var producto = BuscarPorId(id);
        producto.Ingresar(cantidad);
    }

    public void RegistrarEgreso(string id, int cantidad)
    {
        var producto = BuscarPorId(id);
        producto.Egresar(cantidad);
    }

    public void Eliminar(string id)
    {
        var producto = BuscarPorId(id);
        if (producto.Cantidad > 0)
        {
            throw new InvalidOperationException("No se puede eliminar un producto con cantidades disponibles.");
        }
        _productos.Remove(producto);
    }
}