namespace GestionInventario.Dominio;

public class Producto
{
    //Propiedades
    public string Nombre { get; }
    public string Id { get; }
    public string Categoria { get; }
    public int StockMinimo { get; }
    public int StockMaximo { get; }
    public int Cantidad { get; private set; }

    //Constructor
    public Producto(string nombre, string id, string categoria, int stockMinimo, int stockMaximo)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("El ID no puede estar vacío");
        }
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede estar vacío");
        }
        if (stockMinimo<0)
        {
            throw new ArgumentException("El stock minimo no puede ser negativo");
        }
        if (stockMaximo<=stockMinimo)
        {
            throw new ArgumentException("El stock maximo no puede ser menor que el stock minimo");
        }
        Nombre = nombre;
        Id = id;
        Categoria = categoria;
        StockMinimo = stockMinimo;
        StockMaximo = stockMaximo;
        Cantidad = 0;

    }

    public void Ingresar(int cantidad)
    {
        if (cantidad<=0)
        {
            throw new ArgumentException("Debe ingresar una cantidad positiva");
        }
        
        Cantidad+=cantidad;
    }

    public void Egresar(int cantidad)
    {
        if (cantidad<=0)
        {
            throw new ArgumentException("Debe ingresar una cantidad positiva");
        }
        if (Cantidad - cantidad < 0)
        {
            throw new InvalidOperationException("No puede egresar más cantidad de la disponible");
        }
        Cantidad-=cantidad;
    }
    public bool SobreStock => Cantidad > StockMaximo;
    public bool FaltaStock => Cantidad <= StockMinimo;

}