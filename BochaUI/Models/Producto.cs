namespace BochaUI.Models
{ 
    public class Producto
    {
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public string? ImagenProductoURL { get; set; }
        public double Precio { get; set; }
        public Guid IdMarca { get; set; }
        public Guid IdCategoria { get; set; }

    }
}