namespace Proyecto_DSWI.Models
{
    public class CarritoItems
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public CarritoItems()
        {

        }
        public CarritoItems(Producto producto, int cantidad)
        {
            this.Producto = producto;
            this.Cantidad = cantidad;
        }
    }
}
