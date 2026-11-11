using GestionInventario.Dominio;
using GestionInventario.Presentacion;

var inventario = new Inventario();
var menu = new MenuConsola(inventario);
menu.Ejecutar();